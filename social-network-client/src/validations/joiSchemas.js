import Joi from 'joi';

export const LoginSchema = {
  username: Joi.string()
    .required()
    .label('Username'),
  password: Joi.string()
    .min(8)
    .required()
    .label('Password')
};

export const RegisterSchema = {
  username: Joi.string()
    .required()
    .label('Username'),
  email: Joi.string()
    .email()
    .required()
    .label('Email'),
  password: Joi.string()
    .min(8)
    .required()
    .label('Password'),
  password2: Joi.string()
    .min(8)
    .required()
    .valid(Joi.ref('password'))
    .options({
      language: {
        any: {
          allowOnly: '!!Passwords do not match'
        }
      }
    })
    .label('Confirm password')
};

export const ResetPasswordSchema = {
  email: Joi.string()
    .email()
    .required()
    .label('Email'),
  password: Joi.string()
    .min(8)
    .required()
    .label('Password'),
  password2: Joi.string()
    .min(8)
    .required()
    .valid(Joi.ref('password'))
    .options({
      language: {
        any: {
          allowOnly: '!!Passwords do not match'
        }
      }
    })
    .label('Confirm password')
};
