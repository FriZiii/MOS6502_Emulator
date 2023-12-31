import { useState } from 'react';
import { NavigationContainer } from '@react-navigation/native'
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs'
import Ionicons from 'react-native-vector-icons/Ionicons';

import Instructionscreen from './pages/instruction'
import Emulatorscreen from './pages/emulator'

const App = () => {
  const [programInput, setProgramInput] = useState('');

  const emulatorName = 'Emulator'
  const instructionName = 'Instruction'
  const Tab = createBottomTabNavigator()
  return (
    <NavigationContainer>
      <Tab.Navigator
        initialRouteName={emulatorName}
        screenOptions={({ route }) => ({
          tabBarIcon: ({ focused, color, size }) => {
            let iconName
            let routeName = route.name
            if (routeName === emulatorName) {
              iconName = focused ? 'settings' : 'settings-outline'
            } else if (routeName === instructionName) {
              iconName = focused ? 'library' : 'library-outline'
            }
            return <Ionicons name={iconName} size={size} color={color} />;
          },
          tabBarActiveTintColor: 'black',
          tabBarInactiveTintColor: 'gray',
          tabBarLabelStyle: { paddingBottom: 10, fontSize: 12 },
          tabBarStyle: { padding: 10, height: 70 },
          tabBarItemStyle: { flexDirection: 'column' },
        })}>

        <Tab.Screen name={emulatorName} options={{ headerShown: false }} >
          {() => <Emulatorscreen setProgramInput={setProgramInput} programInput={programInput} />}
        </Tab.Screen>
        <Tab.Screen name={instructionName} options={{ headerShown: false }} >
        {() => <Instructionscreen setProgramInput={setProgramInput} />}
        </Tab.Screen>

      </Tab.Navigator>
    </NavigationContainer>
  );
}
export default App;