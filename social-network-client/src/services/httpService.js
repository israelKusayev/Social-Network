export function Get(url, jwt = null) {
  let myHeaders = new Headers({
    'Content-Type': 'application/json'
  });
  if (jwt) myHeaders.append('x-auth-token', jwt);
  return fetch(url, { headers: myHeaders });
}

export function Post(url, data, jwt = null) {
  let myHeaders = new Headers({
    'Content-Type': 'application/json'
  });
  if (jwt) myHeaders.append('x-auth-token', jwt);
  return fetch(url, {
    method: 'POST',
    body: data,
    headers: myHeaders
  });
}

export function Put(url, data, jwt = null) {
  let myHeaders = new Headers({
    'Content-Type': 'application/json'
  });
  if (jwt) myHeaders.append('x-auth-token', jwt);
  console.log(jwt);

  return fetch(url, {
    method: 'PUT',
    body: data,
    headers: myHeaders
  });
}
