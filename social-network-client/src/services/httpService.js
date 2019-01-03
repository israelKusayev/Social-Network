const myHeaders = new Headers({
  'Content-Type': 'application/json'
});

export function Get(url, jwt = null) {
  if (jwt) myHeaders.append('x-auth-token', jwt);
  return fetch(url, { headers: myHeaders });
}

export function Post(url, data, jwt = null) {
  if (jwt) myHeaders.append('x-auth-token', jwt);
  return fetch(url, {
    method: 'POST',
    body: data,
    headers: myHeaders
  });
}

export function Put(url, data, jwt = null) {
  if (jwt) myHeaders.append('x-auth-token', jwt);
  return fetch(url, {
    method: 'PUT',
    body: data,
    headers: myHeaders
  });
}
