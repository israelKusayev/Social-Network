import { getJwt, setJwt, deleteJwt } from './jwtService';
const authUrl = process.env.REACT_APP_AUTH_URL;

export async function Get(url, jwt = false) {
  let myHeaders = new Headers({
    'Content-Type': 'application/json'
  });
  if (jwt) myHeaders.append('x-auth-token', getJwt());
  try {
    const res = await fetch(url, { headers: myHeaders });
    if (jwt && res.status === 401) {
      await refreshToken();
      return Get(url, jwt);
    }
    return res;
  } catch (err) {
    return err;
  }
}

export async function Post(url, data, jwt = false) {
  let myHeaders = new Headers({
    'Content-Type': 'application/json'
  });
  if (jwt) myHeaders.append('x-auth-token', getJwt());
  try {
    const res = await fetch(url, {
      method: 'POST',
      body: data,
      headers: myHeaders
    });
    if (jwt && res.status === 401) {
      await refreshToken();
      return Post(url, data, jwt);
    }
    return res;
  } catch (err) {
    return err;
  }
}

export async function Put(url, data, jwt = false) {
  let myHeaders = new Headers({
    'Content-Type': 'application/json'
  });
  if (jwt) myHeaders.append('x-auth-token', getJwt());
  try {
    const res = await fetch(url, {
      method: 'PUT',
      body: data,
      headers: myHeaders
    });
    if (jwt && res.status === 401) {
      await refreshToken();
      return Put(url, data, jwt);
    }
    return res;
  } catch (err) {
    return err;
  }
}

export async function refreshToken() {
  const res = await Get(authUrl + 'refreshToken', getJwt());
  if (res.status === 200) {
    const jwt = res.headers.get('x-auth-token');

    setJwt(jwt);
  } else {
    deleteJwt();
    window.location.reload();
  }
}
