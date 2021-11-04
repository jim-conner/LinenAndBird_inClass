import axios from "axios";
import firebaseConfig from "../config";

const baseUrl = firebaseConfig.localDatabaseURL;

const getAllBirds = () => new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/birds`)
      .then(response => resolve(response.data))
      .catch(error => reject(error))
  });

export default getAllBirds;
