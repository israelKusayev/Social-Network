import { Post, Put } from './httpService';

const authUrl = process.env.REACT_APP_AUTH_URL;
const tokenKey = 'token';

export async function register(data) {
  const res = await Post(authUrl + 'register', data);

  if (res.status !== 200) throw new Error();

  const jwt = res.headers.get('x-auth-token');
  if (jwt) {
    setJwt(jwt);
  }
}

export async function login(data) {
  const res = await Post(authUrl + 'login', data);

  if (res.status !== 200) throw new Error();

  const jwt = res.headers.get('x-auth-token');
  setJwt(jwt);
}

export async function facebookLogin(facebookToken) {
  const data = JSON.stringify({
    FacebookId: facebookToken.id,
    Username: facebookToken.name,
    Email: facebookToken.email
  });

  const res = await Post(authUrl + 'loginFacebook', data);

  if (res.status !== 200) throw new Error();

  const jwt = res.headers.get('x-auth-token');
  setJwt(jwt);
}

export async function resetPassword(data) {
  const res = await Put(authUrl + 'resetPassword', data);

  if (res.status !== 200) throw new Error();
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
