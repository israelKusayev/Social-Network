import jwtDecode from 'jwt-decode';
const tokenKey = 'token';

export function getJwt() {
  return localStorage.getItem(tokenKey);
}

export function setJwt(jwt) {
  localStorage.setItem(tokenKey, jwt);
}

export function deleteJwt() {
  localStorage.removeItem(tokenKey);
}

export function getUserId() {
  const payload = jwtDecode(getJwt());
  return payload.sub;
}
export function getUsername() {
  const payload = jwtDecode(getJwt());
  return payload.username;
}
