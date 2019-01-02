import { Post, Put } from './httpService';

const tokenKey = 'token';

export function register(data) {
  // return new Promise(function(resolve, reject) {
  //   Post('api/register', data)
  //     .then((res) => {
  //       const jwt = res.headers.get('x-auth-token');
  //       setJwt(jwt);
  //       debugger;
  //       resolve(1);
  //     })
  //     .catch((err) => {
  //       debugger;
  //       reject(err); // reject
  //     });

  Post('api/register', data)
    .then((res) => {
      const jwt = res.headers.get('x-auth-token');
      setJwt(jwt);
    })
    .catch((err) => {});
}

export function login(data) {
  Post('api/login', data).then((res) => {
    const jwt = res.headers.get('x-auth-token');
    setJwt(jwt);
  });
}

export function facebookLogin(facebookToken) {
  console.log(facebookToken);
  const data = JSON.stringify({
    FacebookId: facebookToken.id,
    Username: facebookToken.name,
    Email: facebookToken.email
  });
  console.log(data);

  Post('api/loginFacebook', data).then((res) => {
    const jwt = res.headers.get('x-auth-token');
    setJwt(jwt);
  });
}

export function resetPassword(data) {
  console.log(data);
  Put('api/resetPassword', data).then((res) => {});
}

export function getJwt() {
  return localStorage.getItem(tokenKey);
}

function setJwt(jwt) {
  localStorage.setItem(tokenKey, jwt);
}

export function logout() {
  localStorage.removeItem(tokenKey);
}
