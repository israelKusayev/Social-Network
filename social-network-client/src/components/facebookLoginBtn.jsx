import React from 'react';
import FacebookLogin from 'react-facebook-login';
import '../styles/facebookButton.css';

export default function FacebookLoginBtn(props) {
  return (
    <FacebookLogin
      cssClass="loginBtn loginBtn--facebook"
      textButton="Login with facebook"
      appId="529273954223639"
      callback={props.facebookLogin}
      size="medium"
      fields="name,email,picture"
    />
  );
}
