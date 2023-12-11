import * as React from 'react'
import {View, Text, StyleSheet, Pressable } from 'react-native'
import { GestureHandlerRootView, ScrollView } from 'react-native-gesture-handler'

export default function Instruction({naviagtion}) {
    return (
        <GestureHandlerRootView  style={styles.container}>
            <ScrollView>
                <Text style={styles.header}>Instructions </Text>
                <View>
                    <Pressable style={styles.buttonStyle}><Text style={styles.buttonTextStyle}>Addition</Text></Pressable>
                    <Text style={styles.operationDescText}>This instruction will load 5 into accumulator (LDA #5), then add 7 to accumulator (ADC #7) and at the end will load number from accumulator to memory address 0003 (STA $1E)</Text>
                    <Pressable style={styles.buttonStyle}><Text style={styles.buttonTextStyle}>Subtraction</Text></Pressable>
                    <Text style={styles.operationDescText}>This instruction will load 8 into accumulator (LDA #8), then subtract 3 from accumulator (SBC #3) and at the end will load number from accumulator to memory address 0003 (STA $1E)</Text>
                    <Pressable style={styles.buttonStyle}><Text style={styles.buttonTextStyle}>Multiplication</Text></Pressable>
                    <Text style={styles.operationDescText}>This instruction will load 20 into accumulator (LDA #20), then shift 1 bit left on the number in accumulator (ASL) and at the end will load number from accumulator to memory address 0003 (STA $1E)</Text>
                    <Pressable style={styles.buttonStyle}><Text style={styles.buttonTextStyle}>Division</Text></Pressable>
                    <Text style={styles.operationDescText}>This instruction will load 8 into accumulator (LDA #8), then shift 1 bit right on the number in accumulator (LSR) and at the end will load number from accumulator to memory address 0003 (STA $1E)</Text>
                </View>
                <View>
                    <Text style={styles.header2}>Supported operations</Text>
                    <View style={styles.instructionList}>
                        <Text>&bull; ADC - Addition</Text>
                        <Text>&bull; SBC - Subtraction</Text>
                        <Text>&bull; AND - Logical operator and</Text>
                        <Text>&bull; BIT - Addition</Text>
                        <Text>&bull; EOR - Logical operator eor</Text>
                        <Text>&bull; ORA - Logical operator or</Text>
                        <Text>&bull; CLC - Clear carry flag </Text>
                        <Text>&bull; CLD - Clear decimal flag </Text>
                        <Text>&bull; CLV - Clear overflow flag </Text>
                        <Text>&bull; CMP - Compare memory with accumulator </Text>
                        <Text>&bull; CPX - Compare memory with X </Text>
                        <Text>&bull; CPY - Compare memory with Y </Text>
                        <Text>&bull; DEC - Decrement memory</Text>
                        <Text>&bull; DEX - Decrement X</Text>
                        <Text>&bull; DEY - Decrement Y</Text>
                        <Text>&bull; INC - Increment memory</Text>
                        <Text>&bull; INX - Increment X</Text>
                        <Text>&bull; INY - Increment Y</Text>
                        <Text>&bull; LDA - Load accumulator with memory</Text>
                        <Text>&bull; LDX - Load accumulator with X</Text>
                        <Text>&bull; LDY - Load accumulator with Y</Text>
                        <Text>&bull; SEC - Sets carry flag</Text>
                        <Text>&bull; SED - Sets decimal flag</Text>
                        <Text>&bull; ASL - Shifts bits left</Text>
                        <Text>&bull; LSR - Shifts bits right</Text>
                        <Text>&bull; ROL - Shifts bits left</Text>
                        <Text>&bull; ROR - Shifts bits right</Text>
                        <Text>&bull; STA - Store accumulator in memory </Text>
                        <Text>&bull; STX - Store accumulator in X </Text>
                        <Text>&bull; STY - Store accumulator in Y </Text>
                        <Text>&bull; TAX - Transfer accumulator to X </Text>
                        <Text>&bull; TAY - Transfer accumulator to Y </Text>
                        <Text>&bull; TXA - Transfer X to accumulator </Text>
                        <Text>&bull; TYA - Transfer Y to accumulator</Text>
                        <Text>&bull; NOP - No operation</Text>
                    </View>
                    <Text style={styles.header2}>Supported addresing modes</Text>
                    <View>
                        <Text>&bull; Immediate - to use you need to write # before an opcode (ex. LDA #20)</Text>
                        <Text>&bull; Zeropage - to use you need to write $ before an opcode (ex. STA $1E)</Text>
                        <Text>&bull; Implied - to use you only write an instruction (ex. CLV)</Text>
                        <Text>&bull; Accumulator - to use you only write an instruction (ex. ROR)</Text>
                    </View>
                </View>
            </ScrollView>
        </GestureHandlerRootView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 12,
        marginTop: 20,
    },
    header: {
        marginTop: 8,
        fontSize: 28,
        fontWeight: '500',
        textAlign: 'center',
        marginBottom: 8,
    },
    header2: {
        fontSize: 22,
        textAlign: 'center',
        marginTop: 30,
        marginBottom: 10,
    },
    buttonStyle: {
        alignItems: 'center',
        justifyContent: 'center',
        paddingVertical: 10,
        paddingHorizontal: 10,
        borderRadius: 5,
        backgroundColor: 'black',
        marginVertical: 8,
        marginTop: 30,
    },
    buttonTextStyle: {
        fontSize: 16,
        lineHeight: 21,
        fontWeight: 'bold',
        letterSpacing: 0.25,
        color: 'white',
    },
    operationDescText: {
        paddingHorizontal: 10,
        fontSize: 14.5,
    },
    instructionList: {
    },
});