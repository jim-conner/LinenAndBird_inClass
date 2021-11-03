import axios from "axios";
import config from "../config";

const baseUrl = config.baseUrl;

const getAllBirds = {
  new Promise((resolve, reject) => {
    axios.get(`${baseUrl}/api/birds`)
      .then(response => resolve(response.data))
      .catch(error => reject(error))
  })
}

export default getAllBirds;
