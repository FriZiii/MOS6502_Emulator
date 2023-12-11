import React, { useMemo, useState } from 'react';
import { View, Text, StyleSheet, Button, TextInput, Pressable } from 'react-native';
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
                <Pressable style={styles.buttonLeftStyle}><Text style={styles.buttonTextStyle}>Load Program</Text></Pressable>
                <Pressable style={styles.buttonRightStyle}><Text style={styles.buttonTextStyle}>Next Step</Text></Pressable>
            </View>

            <Text style={styles.propertiesheader}>Processor State</Text>
            <View style={styles.propertiesContainerStyle}>
              <View style={styles.propertyStyle}>
                  <Text style={styles.popertyTextStyle}>Accumulator</Text><Text>$45</Text>
              </View>
              <View style={styles.propertyStyle}>
                  <Text style={styles.popertyTextStyle}>Counter</Text><Text>5</Text>
              </View>
              <View style={styles.propertyStyle}>
                  <Text style={styles.popertyTextStyle}>X Register</Text><Text>5</Text>
              </View>
              <View style={styles.propertyStyle}>
                  <Text style={styles.popertyTextStyle}>Y Register</Text><Text>0</Text>
              </View>
            </View>

            <Text style={styles.propertiesheader}>Flags State</Text>
            <View style={styles.flagsContainerStyle}>
              <View style={styles.flagStyle}>
                  <Text style={styles.flagTextStyle}>Negative Flag</Text><Text>1</Text>
              </View>
              <View style={styles.flagStyle}>
                  <Text style={styles.flagTextStyle}>Overflow Flag</Text><Text>0</Text>
              </View>
              <View style={styles.flagStyle}>
                  <Text style={styles.flagTextStyle}>Decimal Flag</Text><Text>1</Text>
              </View>
              <View style={styles.flagStyle}>
                  <Text style={styles.flagTextStyle}>Zero Flag</Text><Text>0</Text>
              </View>
              <View style={styles.lonelyflagStyle}>
                  <Text style={styles.flagTextStyle}>Carry Flag</Text><Text>0</Text>
              </View>
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
        borderColor: "#BCBCBC",
        marginTop: 20,
        marginBottom: 8,
        borderRadius: 10,
    },
    buttonLeftStyle: {
      flex: 1,
      alignItems: 'center',
      justifyContent: 'center',
      paddingVertical: 10,
      paddingHorizontal: 10,
      borderRadius: 5,
      backgroundColor: 'black',
      marginRight: 10,
    },
    buttonRightStyle: {
      flex: 1,
      alignItems: 'center',
      justifyContent: 'center',
      paddingVertical: 10,
      paddingHorizontal: 10,
      borderRadius: 5,
      backgroundColor: 'black',
      marginLeft: 10,
    },
    buttonTextStyle: {
      fontSize: 16,
      lineHeight: 21,
      fontWeight: 'bold',
      letterSpacing: 0.25,
      color: 'white',
    },
    buttonsContainer: {
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
    propertyStyle: {
      display: 'flex',
      flexDirection: 'row',
      flexBasis: '48%',
      borderRadius: 15,
      display: 'flex',
      justifyContent: 'space-between',
      paddingHorizontal: 15,
      paddingVertical: 5,
      marginVertical: 5,
      backgroundColor: '#fff',
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.22,
      shadowRadius: 2.22,

      elevation: 5,
    },
    popertyTextStyle: {
      fontWeight: 'bold',
    },
    propertiesContainerStyle: {
      display: 'flex',
      flexDirection: 'row',
      flexWrap: 'wrap',
      justifyContent: 'space-between',
    },
    flagsContainerStyle: {
      display: 'flex',
      flexDirection: 'row',
      flexWrap: 'wrap',
      justifyContent: 'space-between',
    },
    flagStyle: {
      display: 'flex',
      flexDirection: 'row',
      flexBasis: '48%',
      borderRadius: 15,
      display: 'flex',
      justifyContent: 'space-between',
      paddingHorizontal: 15,
      paddingVertical: 5,
      marginVertical: 5,
      backgroundColor: '#fff',
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.22,
      shadowRadius: 2.22,

      elevation: 5,
    },
    lonelyflagStyle: {
      display: 'flex',
      flexDirection: 'row',
      flexBasis: '100%',
      borderRadius: 15,
      display: 'flex',
      justifyContent: 'space-between',
      paddingHorizontal: 15,
      paddingVertical: 5,
      marginVertical: 5,
      backgroundColor: '#fff',
      shadowColor: "#000",
      shadowOffset: {
        width: 0,
        height: 2,
      },
      shadowOpacity: 0.22,
      shadowRadius: 2.22,

      elevation: 5,
    },
    propertiesheader: {
      fontSize: 20,
      textAlign: 'center',
      marginTop: 30,
      marginBottom: 5,
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