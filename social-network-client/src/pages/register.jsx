import React, { Component } from 'react';
import Joi from 'joi';
import FacebookLoginBtn from '../components/facebookLoginBtn';
export default class Register extends Component {
  state = {
    data: {
      username: '',
      email: '',
      password: '',
      password2: ''
    },
    error: ''
  };

  schema = {
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
      .label('Confirm password')
  };

  handleChange = ({ currentTarget: input }) => {
    // const errors = { ...this.state.errors };
    // const errorMessage = this.validateProperty(input);
    // if (errorMessage) errors[input.name] = errorMessage;
    // else delete errors[input.name];

    const data = { ...this.state.data };
    data[input.name] = input.value;

    this.setState({ data });
  };

  handleSubmit = (e) => {
    e.preventDefault();

    const { error } = Joi.validate({ ...this.state.data }, this.schema);
    if (error) {
      this.setState({ error: error.details[0].message });
      return;
    }
  };

  facebookLogin = (res) => {
    console.log(res);
  };
  render() {
    return (
      <>
        <h1 className="text-center">Register</h1>
        <form onSubmit={this.handleSubmit}>
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <input
              name="username"
              value={this.state.username}
              onChange={this.handleChange}
              className="form-control"
              type="text"
              id="username"
              placeholder="username"
            />
          </div>
          <div className="form-group">
            <label htmlFor="email">Email address</label>
            <input
              name="email"
              type="email"
              className="form-control"
              id="email"
              aria-describedby="emailHelp"
              placeholder="Enter email"
              value={this.state.email}
              onChange={this.handleChange}
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Password</label>
            <input
              name="password"
              type="password"
              className="form-control"
              id="password"
              placeholder="Password"
              value={this.state.password}
              onChange={this.handleChange}
            />
          </div>
          <div className="form-group">
            <label htmlFor="Confirm password">Confirm password</label>
            <input
              name="password2"
              type="password"
              className="form-control"
              id="Confirm password"
              placeholder="Confirm password"
              value={this.state.password2}
              onChange={this.handleChange}
            />
          </div>
          {this.state.error && <div className="alert alert-danger">{this.state.error}</div>}
          <button type="submit" className="btn btn-primary">
            Register
          </button>
        </form>
        <FacebookLoginBtn facebookLogin={this.facebookLogin} />
      </>
    );
  }
}
