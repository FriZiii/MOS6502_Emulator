namespace Domain.Interfaces
{
    public interface IProcessor
    {
        int CurrentOpCode { get; }
        int ProgramCounter { get; }

        int Accumulator { get; }
        int XRegister { get; }
        int YRegister { get; }

        bool CarryFlag { get; }
        bool DecimalFlag { get; }
        bool NegativeFlag { get; }
        bool OverflowFlag { get; }
        bool ZeroFlag { get; }

        void ClearMemory();
        void ClearState();
        byte[] DumpMemory();
        void Reset();
        void InterruptRequest();
        void LoadProgram(int offset, byte[] program);
        void NextStep();
        void NonMaskableInterrupt();
        byte ReadMemoryValue(int address);
        void WriteMemoryValue(int address, byte data);
    }
}