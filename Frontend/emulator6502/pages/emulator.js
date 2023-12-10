import React, { useMemo, useState } from 'react';
import { View, Text, StyleSheet, Button, TextInput } from 'react-native';
import BottomSheet from '@gorhom/bottom-sheet';
import { GestureHandlerRootView } from 'react-native-gesture-handler'

import MemoryMap from '../components/memory-map';


const Emulator = ({naviagtion}) => {
    const snapPoints = useMemo(() => ['10%', '65%'], []);

    const [programInput, setProgramInput] = useState('');
    return (
        <GestureHandlerRootView  style={styles.container}>
            <TextInput
                placeholder="Enter code..."
                editable
                multiline
                numberOfLines={8}
                onChangeText={text => setProgramInput(text)}
                value={programInput}
                style={styles.input}
            />
            <View style={styles.buttonsContainer}>
                <Button style={styles.buttonStyle} title='Load Program'/>
                <Button style={styles.buttonStyle} title='Next Step'/>
            </View>
        <BottomSheet style={styles.bottomSheet} snapPoints={snapPoints}>
          <View style={styles.contentContainer}>
            <Text>Memmory Map</Text>
            <MemoryMap />
          </View>
        </BottomSheet>
      </GestureHandlerRootView >
    );
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      padding: 24,
    },
    input: {
        textAlignVertical: 'top',
        width: '100%',
        padding: 10,
        borderWidth: 1,
        borderColor: "#BCBCBC"
    },
    buttonStyle: {
        color: 'red',
        alignItems: 'center',
        justifyContent: 'center',
        paddingVertical: 12,
        paddingHorizontal: 32,
        borderRadius: 4,
        elevation: 3,
        backgroundColor: 'black',
    },
    buttonsContainer: {
        flex: 1,
    },
    bottomSheet: {
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.25,
      shadowRadius: 3.84,
      
      elevation: 5,
    },
    contentContainer: {
      flex: 1,
      alignItems: 'center',
    },
  });
  
export default Emulator;