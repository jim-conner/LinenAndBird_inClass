import axios from "axios";
import config from "../config";

const baseUrl = config.baseUrl;

export function getAllBirds(){
  new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/api/birds`)
      .then(response => resolve(response.data))
      .catch(error => reject(error))
  })
}
