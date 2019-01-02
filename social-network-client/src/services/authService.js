import { Post } from './httpService';

const tokenKey = 'token';

export function Register(data) {
  return new Promise(function(resolve, reject) {
    Post('api/register', data)
      .then((res) => {
        const jwt = res.headers.get('x-auth-token');
        setJwt(jwt);
        debugger;
        resolve(1);
      })
      .catch((err) => {
        debugger;
        reject(err); // reject
      });
  });
}

export function Login(data) {
  Post('api/login', data).then((res) => {
    const jwt = res.headers.get('x-auth-token');
    setJwt(jwt);
  });
}

export function FacebookLogin(facebookToken) {
  console.log(facebookToken);
  const data = JSON.stringify({
    FacebookId: facebookToken.id,
    Username: facebookToken.name,
    Email: facebookToken.email
  });
  console.log(data);

  Post('api/LoginFacebook', data).then((res) => {
    const jwt = res.headers.get('x-auth-token');
    setJwt(jwt);
  });
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
