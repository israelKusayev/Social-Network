import { Post, Put } from './httpService';
import { setJwt } from './jwtService';
const authUrl = process.env.REACT_APP_AUTH_URL;
export async function register(data) {
  const res = await Post(authUrl + 'register', data);

  if (res.status !== 200) {
    return await res.json();
  }

  const jwt = res.headers.get('x-auth-token');
  if (jwt) {
    setJwt(jwt);
  }
}

export async function login(data) {
  const res = await Post(authUrl + 'login', data);

  if (res.status !== 200) {
    return await res.json();
  }

  const jwt = res.headers.get('x-auth-token');
  setJwt(jwt);
}

export async function facebookLogin(facebookToken) {
  const res = await Post(authUrl + 'loginFacebook', JSON.stringify(facebookToken.accessToken));

  if (res.status !== 200) {
    return await res.json();
  }

  const jwt = res.headers.get('x-auth-token');
  setJwt(jwt);
}

export async function resetPassword(data) {
  const res = await Put(authUrl + 'resetPassword', data);

  if (res.status !== 200) {
    return await res.json();
  }
}
