import React, { Component } from 'react';
import Joi from 'joi';
import { ResetPasswordSchema as schema } from '../validations/joiSchemas';
import { resetPassword } from '../services/authService';

export default class ResetPassword extends Component {
  state = {
    data: {
      username: '',
      password: '',
      password2: ''
    },
    success: '',
    error: ''
  };

  handleChange = ({ currentTarget: input }) => {
    const data = { ...this.state.data };
    data[input.id] = input.value;
    this.setState({ data });
  };

  handleSubmit = async (e) => {
    e.preventDefault();

    // validate
    const { error } = Joi.validate({ ...this.state.data }, schema);
    if (error) {
      this.setState({ error: error.details[0].message });
      return;
    }
    this.setState({ error: '' });

    //reset password
    const data = JSON.stringify({ username: this.state.data.username, newPassword: this.state.data.password });
    const res = await resetPassword(data);
    if (res.status === 200) this.setState({ error: '', success: 'Password changed successfully' });
    else if (res.status === 400) this.setState({ error: 'invalid username',success:"" });
    else this.setState({ error: 'something faild',success:"" });
  };
  render() {
    const { data, error, success } = this.state;
    return (
      <>
        <h1 className="text-center">Reset password</h1>
        <form className="row" onSubmit={this.handleSubmit}>
          <div className="col-md-6 offset-3">
            <div className="form-group">
              <label htmlFor="username">Username</label>
              <input
                type="text"
                className="form-control"
                id="username"
                placeholder="Username"
                value={data.username}
                onChange={this.handleChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="password">New password</label>
              <input
                type="password"
                className="form-control"
                id="password"
                placeholder="Password"
                value={data.password}
                onChange={this.handleChange}
              />
            </div>
            <div className="form-group">
              <label htmlFor="password2">Confirm new password</label>
              <input
                type="password"
                className="form-control"
                id="password2"
                placeholder="Confirm password"
                value={data.password2}
                onChange={this.handleChange}
              />
            </div>
            {error && <div className="alert alert-danger">{error}</div>}
            {success && <div className="alert alert-success">{success}</div>}
            <button type="submit" className="btn btn-dark">
              Reset
            </button>
          </div>
        </form>
      </>
    );
  }
}
