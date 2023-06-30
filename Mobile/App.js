import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import * as React from 'react';
import MadeScreen from './telas/MadeScreen';
import Login from './telas/Login';
import Sign from './telas/Sign';
import AlunoScreen from './telas/AlunoScreen';
import ProfessorScreen from './telas/ProfessorScreen';




const Stack = createStackNavigator();

function MyStack() {
  return (
    <Stack.Navigator>
      <Stack.Screen name="Login" component={Login} />
      <Stack.Screen name="MadeScreen" component={MadeScreen} />
      <Stack.Screen name="Sign" component={Sign} />
      <Stack.Screen name="ProfessorScreen" component={ProfessorScreen} />
      <Stack.Screen name="AlunoScreen" component={AlunoScreen} />
    </Stack.Navigator>
  );
}

export default function App() {
  return (
    <NavigationContainer>
      <MyStack/>
    </NavigationContainer>
  );
}
