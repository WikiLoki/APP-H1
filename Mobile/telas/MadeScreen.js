import React, { useState } from 'react';
import { View, Text, StyleSheet, TextInput, TouchableOpacity } from 'react-native';
import axios from 'axios';

const MadeScreen = ({ route }) => {
  const { professorName, nomeAluno } = route.params;
  const [tema, setTema] = useState('');
  const [description, setDescription] = useState('');

  const handleSubmit = () => {
    if (tema.trim() === '') {
      alert('Campo tema é obrigatório');
      return;
    }
    if (description.trim() === '') {
      alert('Campo descrição é obrigatório');
      return;
    }

    const novaSolicitacao = {
      tema: tema,
      descricao: description,
      nomeAluno: nomeAluno,
      professorName: professorName,
    };

    axios
      .post('http://192.168.1.3:3000/USERS', novaSolicitacao)
      .then(response => {
        // Solicitação enviada com sucesso, faça o tratamento necessário
        alert('Solicitação enviada com sucesso!');
      })
      .catch(error => {
        // Trate o erro de envio da solicitação
        console.error(error);
        alert('Ocorreu um erro ao enviar a solicitação.');
      });
  };

  return (
    <View style={styles.container}>
      <View style={styles.screenWrapper}>
      <Text style={{ color: 'white'}}>{nomeAluno}</Text>
        <Text style={{ color: 'white', fontSize: 22, fontWeight: 'bold', marginBottom: 20 }}>SOLICITAÇÃO</Text>

        <View style={styles.container}>
          <Text style={styles.label}>Solicitação para {professorName}</Text>
        </View>

        <View style={styles.container}>
          <Text style={styles.label}>Tema:</Text>
          <TextInput
            style={styles.input}
            placeholder="Digite o tema"
            onChangeText={text => setTema(text)}
            value={tema}
          />
        </View>

        <View style={styles.container}>
          <Text style={styles.label}>Descrição do Projeto:</Text>
          <TextInput
            style={styles.textarea}
            placeholder="Digite a descrição"
            onChangeText={text => setDescription(text)}
            value={description}
            multiline
          />
        </View>

        <TouchableOpacity style={styles.button} onPress={handleSubmit}>
          <Text style={styles.buttonTitle}>Enviar solicitação</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 111,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    textAlign: 'center',
    borderRadius: 15,
    padding: 20,
    marginBottom: 10,
  },
  textarea: {
    width: 300,
    height: 120,
    borderWidth: 1,
    borderColor: 'gray',
    borderRadius: 5,
    paddingHorizontal: 10,
    paddingTop: 10,
  },
  infoWrapper: {
    marginBottom: 20,
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
    backgroundColor: 'white',
    padding: 20,
    width: '100%',
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 20,
    borderRadius: 15,
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

export default MadeScreen;
