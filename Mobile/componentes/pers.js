import { StyleSheet } from 'react-native';

export const styles = StyleSheet.create({
    container: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
      backgroundColor: '#FFF',
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
      marginTop: -15
    },
});
