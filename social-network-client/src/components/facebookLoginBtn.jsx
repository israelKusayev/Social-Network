import React from 'react';
import FacebookLogin from 'react-facebook-login';

export default function FacebookLoginBtn(props) {
  return (
    <FacebookLogin appId="529273954223639" callback={props.facebookLogin} size="small" fields="name,email,picture" />
  );
}
