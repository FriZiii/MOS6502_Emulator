import React from "react";
import { StyleSheet, View, Text } from "react-native";


const MemoryMap = ({ memoryMap}) => {
    return (
        <View style={styles.container}>
            {memoryMap && memoryMap.map((item, index) => (
                <View key={index} style={styles.row}>
                    <Text style={styles.item}>{item.location00}</Text>
                    <Text style={styles.item}>{item.location01}</Text>
                    <Text style={styles.item}>{item.location02}</Text>
                    <Text style={styles.item}>{item.location03}</Text>
                    <Text style={styles.item}>{item.location04}</Text>
                    <Text style={styles.item}>{item.location05}</Text>
                    <Text style={styles.item}>{item.location06}</Text>
                    <Text style={styles.item}>{item.location07}</Text>
                    <Text style={styles.item}>{item.location08}</Text>
                    <Text style={styles.item}>{item.location09}</Text>
                    <Text style={styles.item}>{item.location0A}</Text>
                    <Text style={styles.item}>{item.location0B}</Text>
                    <Text style={styles.item}>{item.location0C}</Text>
                    <Text style={styles.item}>{item.location0D}</Text>
                    <Text style={styles.item}>{item.location0E}</Text>
                    <Text style={styles.item}>{item.location0F}</Text>
                </View>
            ))}
        </View>
    )
}

const styles = StyleSheet.create({
    header: {
        fontSize: 18,
        marginBottom: 5,
        fontWeight: '500'
    },
    container: {
        flex: 1,
        flexDirection: 'column',
        alignItems: "center",
        gap: 1
    },
    row: {
        gap: 1,
        flexDirection: 'row',
    },
    item: {
        flex: 1,
        fontSize: 12,
        textAlign: 'center',
        textAlignVertical: 'center',
        width: 20,
        height: 20,
    }
});

export default MemoryMap