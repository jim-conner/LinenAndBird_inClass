import axios from 'axios';
import firebase from 'firebase/app';

// create axios interceptor to pass token from fb authed user to server
// create something that modifies a request as it goes out,
// fadding a header to it with...
axios.interceptors.request.use((request) => {
  const token = sessionStorage.getItem('token');
  
  if (token != null) {
      request.headers.Authorization = `Bearer ${token}`;
  }

  return request;
}, (err) => {
  return Promise.reject(err);
});

//sign in and then ch eck for a new user to post to our api

const signInUser = () => {
  const provider = new firebase.auth.GoogleAuthProvider();
  firebase.auth().signInWithPopup(provider); //this is actually a Promise
  //if user is brand new, grab their contact info from this user obj
  /*if(user is new...){
    const userObject{
      name: jim, etc
    }
  }
  */
  // then send new user to your api and database
  window.location.href = '/' //could do this somewhere else
};

const signOutUser = () => new Promise((resolve, reject) => {
  firebase.auth().signOut().then(resolve).catch(reject);
});

export { signInUser, signOutUser };
