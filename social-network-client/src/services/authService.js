import { Post } from './httpService';

const tokenKey = 'token';

export function Register(data) {
  Post('api/register', data).then((res) => {
    const jwt = res.headers.get('x-auth-token');
    setJwt(jwt);
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
