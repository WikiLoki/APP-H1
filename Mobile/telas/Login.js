import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity } from 'react-native';
import { StyleSheet } from 'react-native';
import { Feather } from '@expo/vector-icons';
import AsyncStorage from '@react-native-async-storage/async-storage';
import api from '../ApiService/api';


const eye = 'eye';
const eyeOff = 'eye-off';

export default function Login({ navigation }) {
  const [txtPassword, setPassword] = useState('')
  const [iconPass, setIconPass] =  useState(eyeOff)
  const [flShowPass, setShowPass] =  useState(true);
  const [txtLogin, setLogin] = useState('')
  const [flLoading, setLoading] = useState(false)
 

async function navigateToHome() {
  setLoading(true);
  if (txtLogin.trim() === '') {
    alert('Campo login é obrigatório');
    setLoading(false);
    return;
  }
  if (txtPassword.trim() === '') {
    alert('Campo senha é obrigatório');
    setLoading(false);
    return;
  }

  try {
    const response = await api.get(`usuarios/autenticar/${txtLogin}/${txtPassword}`);
    console.log(response)
    const user = response.data[0];
    if (user) {
      if (user.professor === 1) {
        navigation.navigate('ProfessorScreen', { nomeProfessor: user.name });
      } else {
        navigation.navigate('AlunoScreen', { nomeAluno: user.name });
      }
      await AsyncStorage.setItem('@nameApp:userName', txtLogin);
    } else {
      alert('Usuário e/ou senha inválido!');
    }
    
  } catch (error) {
    console.error(error);
    alert('Ocorreu um erro ao tentar fazer login.');
  }

  setLoading(false);
}




  function handleChangeIcon() {
         let icone = iconPass == eye ? eyeOff : eye;
         let flShowPassAux = !flShowPass;
         setShowPass(flShowPassAux);
         setIconPass(icone);
     }


  const handleCreateAccount = () => {
    // Redirecionar o usuário para a tela de criação de conta
    navigation.navigate('Sign');
  };

  const styles = StyleSheet.create({
    container: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
      backgroundColor: '#FFF',
    },
    screenWrapper: {
      marginHorizontal: 20,
      marginVertical: 10,
      backgroundColor: '#FF0000',
      borderRadius: 15,
      paddingHorizontal: 10,
      paddingVertical: 30,
      paddingBottom: 50,
    },
    wrapper: {
    backgroundColor: 'white',
    borderRadius: 10,
    padding: 20,
    marginBottom: 10,
  },
    label: {
      fontSize: 20,
      textAlign: 'center',
      margin: 10,
    },
    input: {
      width: 200,
      height: 40,
      padding: 10,
      borderWidth: 1,
      borderColor: 'gray',
      marginBottom: 20,
      borderRadius: 10,
    },
    resultado: {
      fontSize: 24,
      textAlign: 'center',
      margin: 20,
    },
    button: {
      backgroundColor: 'white',
      borderRadius: 10,
      borderWidth: 1,
      borderColor: 'red',
      padding: 10,
      marginTop: 20,
    },
    buttonTitle: {
      color: 'red',
      fontWeight: 'bold',
      textAlign: 'center',
    },
    redBackground: {
      backgroundColor: 'red',
      padding: 20,
      width: '100%',
      alignItems: 'center',
      justifyContent: 'center',
      marginBottom: 20,
    },
    iconEye: {
      paddingHorizontal: 8,
      
    },
    passwordContainer: {
      flexDirection: 'row',
      alignItems: 'center',
      height: 40,
      borderColor: 'gray',
      borderWidth: 1,
      borderRadius: 5,
      paddingHorizontal: 10,
      marginBottom: 10,
    },
    passwordInput: {
      flex: 1,
    },
});

  return (
    <View style={styles.container}>
      <View style={styles.screenWrapper}>
    <View style={styles.redBackground}>
      <Text style={{ color: 'white', fontSize: 30 , fontWeight: 'bold' }}>TELA DE LOGIN</Text>
      </View>

      <View style={styles.wrapper}>
  <Text style={styles.label}>E-MAIL:</Text>
  <View style={styles.passwordContainer}>
  <TextInput
    placeholder="Digite seu e-mail"
    onChangeText={text => setLogin(text)}
    value={txtLogin}
    keyboardType="email-address"
    
  />
  </View>
  
  <Text style={styles.label}>SENHA:</Text>
  <View style={styles.passwordContainer}>
    <TextInput
      style={styles.passwordInput}
      placeholder="Digite sua senha"
      onChangeText={text => setPassword(text)}
      value={txtPassword}
      secureTextEntry={flShowPass}
    />
    <Feather
      style={styles.iconEye}
      name={iconPass}
      size={22}
      onPress={handleChangeIcon}
    />
  </View>
</View>

          
          
      <TouchableOpacity onPress={navigateToHome} style={styles.button}>
        <Text style={styles.buttonTitle}>REALIZAR LOGIN</Text>
      </TouchableOpacity>
      <TouchableOpacity onPress={handleCreateAccount}> 
      <Text style={{ textDecorationLine: 'underline', marginTop: 10, textAlign: 'center', fontWeight: 'bold', color: 'white'}}>CRIAR CONTA</Text>

      </TouchableOpacity>
    
    </View>
    </View>
  );
}
