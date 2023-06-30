import React, { useEffect, useState } from 'react';
import { View, Text, ScrollView, StyleSheet, TouchableOpacity } from 'react-native';
import api from '../ApiService/api';


const AlunoScreen = ({navigation , route}) => {
  const [professores, setProfessores] = useState([]);
  const { nomeAluno } = route.params;

  useEffect(() => {
    fetch(`${api}/usuarios`)
      .then(response => response.json())
      .then(data => {
        const professoresFiltrados = data.filter(user => user.professor === 1);
        setProfessores(professoresFiltrados);
      })
      .catch(error => console.error(error));
  }, []);

  const navigateMade = (professorName, nomeAluno) => {
    navigation.navigate('MadeScreen', { professorName, nomeAluno });
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
    professorItem: {
      backgroundColor: '#FFF',
      borderRadius: 10,
      borderWidth: 1,
      borderColor: '#CCC',
      padding: 10,
      marginTop: 10,
      width: '90%',
      alignSelf: 'center',
    },
    professorName: {
      fontSize: 18,
    },
  });

  return (
    <ScrollView>
      <View style={styles.container}>
      <View style={styles.screenWrapper}>  
        
      <Text style={{ color: 'white'}}>{nomeAluno}</Text>
      <Text style={{ color: 'white', fontSize: 22, fontWeight: 'bold'}}>ESCOLHA UM ORIENTADOR</Text>
      <Text style={{ color: 'white', marginBottom: 20 }}>O professor escolhido será seu orientador durante todo o projeto, não é possível realizar uma troca posteriormente.</Text>
    
      <View style={styles.container}>
        <Text style={styles.label}>PARA QUAL PROFESSOR DESEJA FAZER SUA SOLICITAÇÃO?</Text>
        {professores.map(professor => (
          <TouchableOpacity onPress={() => navigateMade(professor.name, nomeAluno)} key={professor.id} style={styles.professorItem}>
          <Text style={styles.professorName}>{professor.name}</Text>
        </TouchableOpacity>
        
        
        
        ))}
      </View>
      </View>
      </View>
    </ScrollView>
  );
};

export default AlunoScreen;