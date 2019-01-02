const urlEndPoint = process.env.REACT_APP_API_URl;

export function Get(url) {
  return fetch(urlEndPoint + url);
}

export function Post(url, data) {
  return fetch(urlEndPoint + url, {
    method: 'POST',
    body: data,
    headers: {
      'Content-Type': 'application/json'
    }
  });
}

export function Put(url, data) {
  return fetch(urlEndPoint + url, {
    method: 'PUT',
    body: data,
    headers: {
      'Content-Type': 'application/json'
    }
  });
}
