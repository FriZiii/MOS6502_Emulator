using Domain.Interfaces;

namespace Processor
{
    public class Processor : IProcessor
    {
        #region Fields
        private int _programCounter;
        private bool _nmiTriggered;
        private bool _irqTriggered;
        #endregion

        #region Properties
        protected byte[] Memory { get; private set; }
        public int Accumulator { get; protected set; }
        public int XRegister { get; private set; }
        public int YRegister { get; private set; }
        public int CurrentOpCode { get; private set; }
        public int ProgramCounter
        {
            get { return _programCounter; }
            private set { _programCounter = WrapProgramCounter(value); }
        }
        #endregion

        #region Flags
        public bool CarryFlag { get; protected set; }
        public bool ZeroFlag { get; private set; }
        public bool DecimalFlag { get; private set; }
        public bool OverflowFlag { get; protected set; }
        public bool NegativeFlag { get; private set; }
        #endregion

        public Processor()
        {
            Memory = new byte[0x10000];
        }
        public void Reset()
        {
            ProgramCounter = 0xFFFC;
            ProgramCounter = (Memory[ProgramCounter] | (Memory[ProgramCounter + 1] << 8));
            CurrentOpCode = Memory[ProgramCounter];
        }

        public void NextStep()
        {
            ProgramCounter++;

            ExecuteOpCode();

            CurrentOpCode = ReadMemoryValue(ProgramCounter);

            if (_nmiTriggered)
            {
                NmiOccurred();
                _nmiTriggered = false;
            }
            else if (_irqTriggered)
            {
                IrqTriggered();
                _irqTriggered = false;
            }
        }

        public void LoadProgram(int offset, byte[] program)
        {
            if (offset > Memory.Length)
                throw new InvalidOperationException("Offset '{0}' is larger than memory size '{1}'");

            if (program.Length > Memory.Length + offset)
                throw new InvalidOperationException(string.Format("Program Size '{0}' Cannot be Larger than Memory Size '{1}' plus offset '{2}'", program.Length, Memory.Length, offset));

            for (var i = 0; i < program.Length; i++)
            {
                Memory[i + offset] = program[i];
            }

            Reset();
        }

        public void InterruptRequest()
        {
            _irqTriggered = true;
        }
        public void NonMaskableInterrupt()
        {
            _nmiTriggered = true;
        }

        public void ClearMemory()
        {
            for (var i = 0; i < Memory.Length; i++)
                Memory[i] = 0x00;
        }
        public void ClearState()
        {
            ProgramCounter = 0;
            Accumulator = 0;
            XRegister = 0;
            YRegister = 0;
        }

        public virtual byte ReadMemoryValue(int address)
        {
            var value = Memory[address];
            return value;
        }
        public virtual void WriteMemoryValue(int address, byte data)
        {
            Memory[address] = data;
        }
        public byte[] DumpMemory()
        {
            return Memory;
        }

        #region Private Methods
        private void ExecuteOpCode()
        {
            switch (CurrentOpCode)
            {
                #region Add / Subtract Operations
                //ADC Add With Carry, Immediate
                case 0x69:
                    {
                        AddWithCarryOperation(AddressingMode.Immediate);
                        break;
                    }
                //SBC Subtract with Borrow, Immediate
                case 0xE9:
                    {
                        SubtractWithBorrowOperation(AddressingMode.Immediate);
                        break;
                    }
                #endregion

                #region BitWise Comparison Operations
                //AND Compare Memory with Accumulator, Immediate
                case 0x29:
                    {
                        AndOperation(AddressingMode.Immediate);
                        break;
                    }
                //BIT Compare Memory with Accumulator, Zero Page
                case 0x24:
                    {
                        BitOperation(AddressingMode.ZeroPage);
                        break;
                    }
                //EOR Exclusive OR Memory with Accumulator, Immediate
                case 0x49:
                    {
                        EorOperation(AddressingMode.Immediate);
                        break;
                    }
                //ORA Compare Memory with Accumulator, Immediate
                case 0x09:
                    {
                        OrOperation(AddressingMode.Immediate);
                        break;
                    }
                #endregion

                #region Clear Flag Operations
                //CLC Clear Carry Flag, Implied
                case 0x18:
                    {
                        CarryFlag = false;
                        break;
                    }
                //CLD Clear Decimal Flag, Implied
                case 0xD8:
                    {
                        DecimalFlag = false;
                        break;

                    }
                //CLV Clear Overflow Flag, Implied
                case 0xB8:
                    {
                        OverflowFlag = false;
                        break;
                    }

                #endregion

                #region Compare Operations
                //CMP Compare Accumulator with Memory, Immediate
                case 0xC9:
                    {
                        CompareOperation(AddressingMode.Immediate, Accumulator);
                        break;
                    }
                //CPX Compare Accumulator with X Register, Immediate
                case 0xE0:
                    {
                        CompareOperation(AddressingMode.Immediate, XRegister);
                        break;
                    }
                //CPY Compare Accumulator with Y Register, Immediate
                case 0xC0:
                    {
                        CompareOperation(AddressingMode.Immediate, YRegister);
                        break;
                    }
                #endregion

                #region Increment/Decrement Operations
                //DEC Decrement Memory by One, Zero Page
                case 0xC6:
                    {
                        ChangeMemoryByOne(AddressingMode.ZeroPage, true);
                        break;
                    }
                //DEX Decrement X Register by One, Implied
                case 0xCA:
                    {
                        ChangeRegisterByOne(true, true);
                        break;
                    }
                //DEY Decrement Y Register by One, Implied
                case 0x88:
                    {
                        ChangeRegisterByOne(false, true);
                        break;
                    }
                //INC Increment Memory by One, Zero Page
                case 0xE6:
                    {
                        ChangeMemoryByOne(AddressingMode.ZeroPage, false);
                        break;
                    }
                //INX Increment X Register by One, Implied
                case 0xE8:
                    {
                        ChangeRegisterByOne(true, false);
                        break;
                    }
                //INY Increment Y Register by One, Implied
                case 0xC8:
                    {
                        ChangeRegisterByOne(false, false);
                        break;
                    }
                #endregion

                #region Load Value From Memory Operations
                //LDA Load Accumulator with Memory, Immediate
                case 0xA9:
                    {
                        Accumulator = ReadMemoryValue(GetAddressByAddressingMode(AddressingMode.Immediate));
                        SetZeroFlag(Accumulator);
                        SetNegativeFlag(Accumulator);
                        break;
                    }
                //LDX Load X with memory, Immediate
                case 0xA2:
                    {
                        XRegister = ReadMemoryValue(GetAddressByAddressingMode(AddressingMode.Immediate));
                        SetZeroFlag(XRegister);
                        SetNegativeFlag(XRegister);
                        break;
                    }
                //LDY Load Y with memory, Immediate
                case 0xA0:
                    {
                        YRegister = ReadMemoryValue(GetAddressByAddressingMode(AddressingMode.Immediate));
                        SetZeroFlag(YRegister);
                        SetNegativeFlag(YRegister);
                        break;
                    }
                #endregion

                #region Set Flag Operations
                //SEC Set Carry, Implied
                case 0x38:
                    {
                        CarryFlag = true;
                        break;
                    }
                //SED Set Interrupt, Implied
                case 0xF8:
                    {
                        DecimalFlag = true;
                        break;
                    }
                #endregion

                #region Shift/Rotate Operations
                //ASL Shift Left 1 Bit Memory or Accumulator, Accumulator
                case 0x0A:
                    {
                        AslOperation();
                        break;
                    }
                //LSR Shift Left 1 Bit Memory or Accumulator, Accumulator
                case 0x4A:
                    {
                        LsrOperation();
                        break;
                    }
                //ROL Rotate Left 1 Bit Memory or Accumulator, Accumulator
                case 0x2A:
                    {
                        RolOperation();
                        break;
                    }
                //ROR Rotate Right 1 Bit Memory or Accumulator, Accumulator
                case 0x6A:
                    {
                        RorOperation();
                        break;
                    }
                #endregion

                #region Store Value In Memory Operations
                //STA Store Accumulator In Memory, Zero Page
                case 0x85:
                    {
                        WriteMemoryValue(GetAddressByAddressingMode(AddressingMode.ZeroPage), (byte)Accumulator);
                        break;
                    }
                //STX Store Index X, Zero Page
                case 0x86:
                    {
                        WriteMemoryValue(GetAddressByAddressingMode(AddressingMode.ZeroPage), (byte)XRegister);
                        break;
                    }
                //STY Store Index Y, Zero Page
                case 0x84:
                    {
                        WriteMemoryValue(GetAddressByAddressingMode(AddressingMode.ZeroPage), (byte)YRegister);
                        break;
                    }
                #endregion

                #region Transfer Operations
                //TAX Transfer Accumulator to X Register, Implied
                case 0xAA:
                    {
                        XRegister = Accumulator;
                        SetNegativeFlag(XRegister);
                        SetZeroFlag(XRegister);
                        break;
                    }
                //TAY Transfer Accumulator to Y Register, Implied
                case 0xA8:
                    {
                        YRegister = Accumulator;
                        SetNegativeFlag(YRegister);
                        SetZeroFlag(YRegister);
                        break;
                    }
                //TXA Transfer X Register to Accumulator, Implied
                case 0x8A:
                    {
                        Accumulator = XRegister;
                        SetNegativeFlag(Accumulator);
                        SetZeroFlag(Accumulator);
                        break;
                    }
                //TYA Transfer Y Register to Accumulator, Implied
                case 0x98:
                    {
                        Accumulator = YRegister;
                        SetNegativeFlag(Accumulator);
                        SetZeroFlag(Accumulator);
                        break;
                    }
                #endregion
                //NOP Operation, Implied
                case 0xEA:
                    {
                        break;
                    }

                default:
                    throw new NotSupportedException(string.Format("The OpCode {0} is not supported", CurrentOpCode));
            }
        }

        private void SetNegativeFlag(int value)
        {
            NegativeFlag = value > 127;
        }
        private void SetZeroFlag(int value)
        {
            ZeroFlag = value == 0;
        }

        private int GetAddressByAddressingMode(AddressingMode addressingMode)
        {
            int address;
            int highByte;
            switch (addressingMode)
            {
                case AddressingMode.Immediate:
                    {
                        return ProgramCounter++;
                    }
                case (AddressingMode.ZeroPage):
                    {
                        address = ReadMemoryValue(ProgramCounter++);
                        return address;
                    }
                default:
                    throw new InvalidOperationException(string.Format("The Address Mode '{0}' does not require an address", addressingMode));
            }
        }

        private int WrapProgramCounter(int value)
        {
            return value & 0xFFFF;
        }

        #region Op Code Operations
        private void AddWithCarryOperation(AddressingMode addressingMode)
        {
            var memoryValue = ReadMemoryValue(GetAddressByAddressingMode(addressingMode));
            var newValue = memoryValue + Accumulator + (CarryFlag ? 1 : 0);


            OverflowFlag = (((Accumulator ^ newValue) & 0x80) != 0) && (((Accumulator ^ memoryValue) & 0x80) == 0);

            if (DecimalFlag)
            {
                newValue = int.Parse(memoryValue.ToString("x")) + int.Parse(Accumulator.ToString("x")) + (CarryFlag ? 1 : 0);

                if (newValue > 99)
                {
                    CarryFlag = true;
                    newValue -= 100;
                }
                else
                {
                    CarryFlag = false;
                }

                newValue = (int)Convert.ToInt64(string.Concat("0x", newValue), 16);
            }
            else
            {
                if (newValue > 255)
                {
                    CarryFlag = true;
                    newValue -= 256;
                }
                else
                {
                    CarryFlag = false;
                }
            }

            SetZeroFlag(newValue);
            SetNegativeFlag(newValue);

            Accumulator = newValue;
        }
        private void AndOperation(AddressingMode addressingMode)
        {
            Accumulator = ReadMemoryValue(GetAddressByAddressingMode(addressingMode)) & Accumulator;

            SetZeroFlag(Accumulator);
            SetNegativeFlag(Accumulator);
        }
        private void AslOperation()
        {
            int value;

            ReadMemoryValue(ProgramCounter + 1);
            value = Accumulator;

            CarryFlag = ((value & 0x80) != 0);

            value = (value << 1) & 0xFE;

            SetNegativeFlag(value);
            SetZeroFlag(value);

            Accumulator = value;
        }
        private void BitOperation(AddressingMode addressingMode)
        {

            var memoryValue = ReadMemoryValue(GetAddressByAddressingMode(addressingMode));
            var valueToCompare = memoryValue & Accumulator;

            OverflowFlag = (memoryValue & 0x40) != 0;

            SetNegativeFlag(memoryValue);
            SetZeroFlag(valueToCompare);
        }
        private void CompareOperation(AddressingMode addressingMode, int comparisonValue)
        {
            var memoryValue = ReadMemoryValue(GetAddressByAddressingMode(addressingMode));
            var comparedValue = comparisonValue - memoryValue;

            if (comparedValue < 0)
                comparedValue += 0x10000;

            SetZeroFlag(comparedValue);

            CarryFlag = memoryValue <= comparisonValue;
            SetNegativeFlag(comparedValue);
        }
        private void ChangeMemoryByOne(AddressingMode addressingMode, bool decrement)
        {
            var memoryLocation = GetAddressByAddressingMode(addressingMode);
            var memory = ReadMemoryValue(memoryLocation);

            WriteMemoryValue(memoryLocation, memory);

            if (decrement)
                memory -= 1;
            else
                memory += 1;

            SetZeroFlag(memory);
            SetNegativeFlag(memory);


            WriteMemoryValue(memoryLocation, memory);
        }
        private void ChangeRegisterByOne(bool useXRegister, bool decrement)
        {
            var value = useXRegister ? XRegister : YRegister;

            if (decrement)
                value -= 1;
            else
                value += 1;

            if (value < 0x00)
                value += 0x100;
            else if (value > 0xFF)
                value -= 0x100;

            SetZeroFlag(value);
            SetNegativeFlag(value);


            if (useXRegister)
                XRegister = value;
            else
                YRegister = value;
        }
        private void EorOperation(AddressingMode addressingMode)
        {
            Accumulator = Accumulator ^ ReadMemoryValue(GetAddressByAddressingMode(addressingMode));

            SetNegativeFlag(Accumulator);
            SetZeroFlag(Accumulator);
        }
        private void LsrOperation()
        {
            int value;

            ReadMemoryValue(ProgramCounter + 1);
            value = Accumulator;

            NegativeFlag = false;

            CarryFlag = (value & 0x01) != 0;

            value = (value >> 1);

            SetZeroFlag(value);
            Accumulator = value;
        }
        private void OrOperation(AddressingMode addressingMode)
        {
            Accumulator = Accumulator | ReadMemoryValue(GetAddressByAddressingMode(addressingMode));

            SetNegativeFlag(Accumulator);
            SetZeroFlag(Accumulator);
        }
        private void RolOperation()
        {
            int value;

            ReadMemoryValue(ProgramCounter + 1);
            value = Accumulator;

            var newCarry = (0x80 & value) != 0;

            value = (value << 1) & 0xFE;

            if (CarryFlag)
                value = value | 0x01;

            CarryFlag = newCarry;

            SetZeroFlag(value);
            SetNegativeFlag(value);

            Accumulator = value;
        }
        private void RorOperation()
        {
            int value;

            ReadMemoryValue(ProgramCounter + 1);
            value = Accumulator;

            var newCarry = (0x01 & value) != 0;

            value = (value >> 1);

            if (CarryFlag)
                value = value | 0x80;

            CarryFlag = newCarry;

            SetZeroFlag(value);
            SetNegativeFlag(value);

            Accumulator = value;
        }
        private void SubtractWithBorrowOperation(AddressingMode addressingMode)
        {
            var memoryValue = ReadMemoryValue(GetAddressByAddressingMode(addressingMode));
            var newValue = DecimalFlag
                               ? int.Parse(Accumulator.ToString("x")) - int.Parse(memoryValue.ToString("x")) - (CarryFlag ? 0 : 1)
                               : Accumulator - memoryValue - (CarryFlag ? 0 : 1);

            CarryFlag = newValue >= 0;

            if (DecimalFlag)
            {
                if (newValue < 0)
                    newValue += 100;

                newValue = (int)Convert.ToInt64(string.Concat("0x", newValue), 16);
            }
            else
            {
                OverflowFlag = (((Accumulator ^ newValue) & 0x80) != 0) && (((Accumulator ^ memoryValue) & 0x80) != 0);

                if (newValue < 0)
                    newValue += 256;
            }

            SetNegativeFlag(newValue);
            SetZeroFlag(newValue);

            Accumulator = newValue;
        }

        private void BreakOperation(int vector)
        {
            ReadMemoryValue(++ProgramCounter);
            ProgramCounter = (ReadMemoryValue(vector + 1) << 8) | ReadMemoryValue(vector);
        }
        private void NmiOccurred()
        {
            ProgramCounter--;
            BreakOperation(0xFFFA);
            CurrentOpCode = ReadMemoryValue(ProgramCounter);
        }
        private void IrqTriggered()
        {
            ProgramCounter--;
            BreakOperation(0xFFFE);
            CurrentOpCode = ReadMemoryValue(ProgramCounter);
        }
        #endregion

        #endregion
    }
}