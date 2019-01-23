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
    try {
        const payload = jwtDecode(getJwt());
        return payload.sub;
    } catch (error) {
        deleteJwt();
        window.location.reload();
    }
}
export function getUsername() {
    try {
        const payload = jwtDecode(getJwt());
        return payload.username;
    } catch (error) {
        deleteJwt();
        window.location.reload();
    }
}
