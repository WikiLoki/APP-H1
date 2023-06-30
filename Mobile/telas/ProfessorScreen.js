import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, StyleSheet, TouchableOpacity } from 'react-native';
import { Feather } from '@expo/vector-icons';
import * as Notifications from "expo-notifications"

const enviarNotificacao = async (aluno, aprovado) => {
  const message = aprovado ? "Projeto aprovado!" : "Projeto recusado!";
  
  const notification = {
    title: "Notificação do App",
    body: `${aluno} - ${message}`,
  };

  await Notifications.scheduleNotificationAsync({
    content: notification,
    trigger: null, // Enviar imediatamente
  });
};


const ProfessorScreen = ({ navigation, route }) => {
  const [alunos, setAlunos] = useState([]);
  const { nomeProfessor } = route.params;

  useEffect(() => {
    fetch('http://192.168.1.3:3000/users')
      .then(response => response.json())
      .then(data => {
        const alunosFiltrados = data.filter(user => user.professorName === nomeProfessor);
        setAlunos(alunosFiltrados);
      })
      .catch(error => {
        console.error(error);
        alert('Ocorreu um erro ao recuperar as solicitações.');
      });
  }, []);

  const handleAprovarProjeto = (aluno) => {
    // Lógica para aprovar o projeto
    enviarNotificacao(aluno, true);
  };
  
  const handleRecusarProjeto = (aluno) => {
    // Lógica para recusar o projeto
    enviarNotificacao(aluno, false);
  };
  

  return (
    <ScrollView>
      <View style={styles.container}>
        <View style={styles.screenWrapper}>
          <Text style={{ color: 'white' }}>{nomeProfessor}</Text>
          <Text style={{ color: 'white', fontSize: 30, fontWeight: 'bold' }}>ALUNOS ORIENTADOS</Text>
          <Text style={{ color: 'white', marginBottom: 20 }}>Esses são os alunos que possuem projetos em andamento com você.</Text>

          <View style={styles.container}>
            <Text style={styles.label}>SOLICITAÇÕES DE ALUNOS:</Text>
            {alunos.map(aluno => (
              <View key={aluno.id} style={styles.alunoItem}>
                <Text style={{ color: 'black' }}>{aluno.nomeAluno}</Text>
                <Text>Tema: {aluno.tema}</Text>
                <Text>Descrição: {aluno.descricao}</Text>
                <View style={styles.iconContainer}>
                      <TouchableOpacity onPress={handleAprovarProjeto}>
                        <Feather name="check" size={20} style={[styles.icon, { color: 'green' }]} />
                      </TouchableOpacity>
                      <TouchableOpacity onPress={handleRecusarProjeto}>
                        <Feather name="x" size={20} style={[styles.icon, { color: 'red' }]} />
                      </TouchableOpacity>
                    </View>
              </View>
            ))}
          </View>
        </View>
      </View>
    </ScrollView>
  );
};




const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    textAlign: 'center',
    borderRadius: 15,
    addingBottom: 50,
    paddingHorizontal: 10,
    paddingVertical: 30,
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
  label: {
    fontSize: 20,
    fontWeight: 'bold',
    paddingHorizontal: 10,
    paddingVertical: 5,
    marginBottom: 10,
    alignItems: 'center',
    textAlign: 'center',
  },
  input: {
    width: 200,
    height: 40,
    padding: 10,
    borderWidth: 1,
    borderColor: 'gray',
    marginBottom: 10,
  },
  resultado: {
    fontSize: 24,
    textAlign: 'center',
    margin: 20,
  },
  button: {
    backgroundColor: '#FFF',
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
  alunoItem: {
    backgroundColor: '#FFF',
    borderRadius: 10,
    borderWidth: 1,
    borderColor: '#CCC',
    padding: 10,
    marginTop: 10,
    width: '90%',
    alignSelf: 'center',
  },
  alunoName: {
    fontSize: 18,
  },
  iconContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 10,
  },
  icon: {
    marginHorizontal: 5,
  },
});

export default ProfessorScreen;
