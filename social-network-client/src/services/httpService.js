const _url = 'http://localhost:62262/';

export function Get(url) {
  return fetch(_url + url);
}

export function Post(url, data) {
  return fetch(_url + url, {
    method: 'POST',
    body: data,
    headers: {
      'Content-Type': 'application/json'
    }
  });
}

export function Put(url, data) {
  return fetch(_url + url, {
    method: 'PUT',
    body: data,
    headers: {
      'Content-Type': 'application/json'
    }
  });
}
