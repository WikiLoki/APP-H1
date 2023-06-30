import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, CheckBox } from 'react-native';
import { StyleSheet } from 'react-native';
import { Feather } from '@expo/vector-icons';
import { SafeAreaView } from 'react-native-safe-area-context';
import api from '../ApiService/api';



const eye = 'eye';
const eyeOff = 'eye-off';



export default function Sign({ navigation }) {

  const [flShowPass, setShowPass] = useState(true);
  const [iconPass, setIconPass] = useState(eyeOff)
  const [txtName, setName] = useState('')
  const [txtDocument, setDocument] = useState('')
  const [txtEmail, setEmail] = useState('')
  const [txtPassword, setPassword] = useState('')
  const [txtPasswordConfirm, setPasswordConfirm] = useState('')
  const [lstErrors, setListErrors] = useState([]);
  const backlogin = () => { navigation.navigate('Login'); };
  const [isProfessorSelected, setIsProfessorSelected] = useState(false);


  function handleChangeIcon() {
    let icone = iconPass == eye ? eyeOff : eye;
    let flShowPassAux = !flShowPass;
    setShowPass(flShowPassAux);
    setIconPass(icone);
  }

  function handleChangeIconConfirm() {
    let icone = iconPass == eye ? eyeOff : eye;
    let flShowPassAux = !flShowPass;
    setShowPass(flShowPassAux);
    setIconPass(icone);
  }

  async function handlePostNewStudent() {
    if (camposPrenchidos()) {
      let objNewStudent = {
        name: txtName,
        password: txtPassword,
        document: txtDocument,
        login: txtEmail,
        professor: isProfessorSelected ? 1 : 0
      };

      const response = await api.post(`/usuarios`, objNewStudent);
      console.log(response)
      alert('Usuario Criado!');
    }
    navigation.navigate('Login')
  }

  function camposPrenchidos() {
    let validacoes = [];
    let retorno = true;
    if (txtName.trim() === '') {
      validacoes.push('Campo Nome é obrigatório');
      retorno = false;
    }
    if (txtEmail.trim() === '') {
      validacoes.push('Campo Email é obrigatório');
      retorno = false;
    }
    if (txtPassword.trim() === '') {
      validacoes.push('Campo Senha é obrigatório');
      retorno = false;
    }
    if (txtPasswordConfirm.trim() === '') {
      validacoes.push('Campo Confirmação de senha é obrigatório');
      retorno = false;
    }
    if (txtDocument.trim() === '') {
      validacoes.push('Campo CPF é obrigatório');
      retorno = false;
    }
    setListErrors(validacoes);
    return retorno;
  }

  function navigateToBack() {
    navigation.goBack();
  }



  const styles = StyleSheet.create({
    container: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
      backgroundColor: '#FFF',
    },
    container2: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
    },
    checkboxContainer: {
      flexDirection: 'row',
      alignItems: 'center',
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
    <SafeAreaView style={styles.container}>
      <View style={styles.screenWrapper}>
        <View style={styles.redBackground}>
          <Text style={{ color: 'white', fontSize: 30, fontWeight: 'bold' }}>TELA DE REGISTRO</Text>
        </View>
        <View style={styles.wrapper}>
          <Text style={{ fontSize: 20 }}>NOME:</Text>
          <View style={styles.passwordContainer}>
            <TextInput
              placeholder="Digite seu nome completo"
              onChangeText={text => setName(text)}
              maxLength={50}
              value={txtName}
            />
          </View>
          <Text style={{ fontSize: 20 }}>CPF:</Text>
          <View style={styles.passwordContainer}>
            <TextInput
              placeholder="Digite seu CPF"
              onChangeText={text => setDocument(text)}
              maxLength={11}
              value={txtDocument}
            />
          </View>

          <Text style={{ fontSize: 20 }}>E-MAIL:</Text>
          <View style={styles.passwordContainer}>
            <TextInput
              placeholder="Digite seu e-mail"
              onChangeText={text => setEmail(text)}
              maxLength={50}
              value={txtEmail}
            />
          </View>

          <Text style={{ fontSize: 20 }}>SENHA:</Text>
          <View style={styles.passwordContainer}>
            <TextInput
              style={styles.passwordInput}
              placeholder="Digite sua senha"
              onChangeText={text => setPassword(text)}
              value={txtPassword}
              secureTextEntry={flShowPass}
              maxLength={50}
            />
            <Feather
              style={styles.iconEye}
              name={iconPass}
              size={22}
              onPress={handleChangeIcon}
            />
          </View>

          <Text style={{ fontSize: 20 }}>CONFIRMAÇÃO:</Text>
          <View style={styles.passwordContainer}>
            <TextInput
              style={styles.passwordInput}
              placeholder="Confirme sua senha"
              onChangeText={text => setPasswordConfirm(text)}
              value={txtPasswordConfirm}
              secureTextEntry={flShowPass}
              maxLength={50}
              
            />

            <Feather
              name={iconPass}
              style={styles.iconEye}
              size={22}
              onPress={handleChangeIconConfirm}
            />
          </View>


          <View style={styles.container2}>
            <View style={styles.checkboxContainer}>
              <Text>{isProfessorSelected ? '  ' : ' É PROFESSOR?  '}</Text>
              <CheckBox
                value={isProfessorSelected}
                onValueChange={(value) => setIsProfessorSelected(value)}
              />

            </View>
          </View>

        </View>




        <TouchableOpacity onPress={handlePostNewStudent} style={styles.button}>
          <Text style={styles.buttonTitle}>Registrar</Text>
        </TouchableOpacity>


        <TouchableOpacity onPress={backlogin}>
          <Text style={{ textDecorationLine: 'underline', marginTop: 10, textAlign: 'center', fontWeight: 'bold', color: 'white' }}>Voltar</Text>
        </TouchableOpacity>

      </View>
    </SafeAreaView>

  );
};
