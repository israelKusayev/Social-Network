import React from 'react';

export default function UserProfile({ updateProfile, onChange, user }) {
  return (
    <div className="container mt-1">
      <div className="row">
        <div className="col-md-1 ">
          <div className="list-group " />
        </div>
        <div className="col-md-10">
          <div className="card bg-dark text-white">
            <div className="card-body">
              <div className="row">
                <div className="col-md-12">
                  <h4>Your Profile</h4>
                  <hr />
                </div>
              </div>
              <div className="row">
                <div className="col-md-12">
                  <form onSubmit={updateProfile}>
                    <div className="form-group row">
                      <label htmlFor="username" className="col-4 col-form-label">
                        User Name
                      </label>
                      <div className="col-8">
                        <input
                          id="username"
                          name="username"
                          className="form-control here"
                          type="text"
                          readOnly={true}
                          value={user.username}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label htmlFor="firstName" className="col-4 col-form-label">
                        First Name
                      </label>
                      <div className="col-8">
                        <input
                          id="firstName"
                          name="firstName"
                          placeholder="First Name"
                          className="form-control here"
                          type="text"
                          value={user.firstName}
                          onChange={onChange}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label htmlFor="lastName" className="col-4 col-form-label">
                        Last Name
                      </label>
                      <div className="col-8">
                        <input
                          id="lastName"
                          name="lastName"
                          placeholder="Last Name"
                          className="form-control here"
                          type="text"
                          value={user.lastName}
                          onChange={onChange}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label htmlFor="address" className="col-4 col-form-label">
                        Address
                      </label>
                      <div className="col-8">
                        <input
                          id="address"
                          name="address"
                          placeholder="Address"
                          className="form-control here"
                          type="text"
                          value={user.address}
                          onChange={onChange}
                        />
                      </div>
                    </div>

                    <div className="form-group row">
                      <label htmlFor="email" className="col-4 col-form-label">
                        Email
                      </label>
                      <div className="col-8">
                        <input
                          id="email"
                          name="email"
                          placeholder="Email"
                          className="form-control here"
                          type="email"
                          value={user.email}
                          onChange={onChange}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label htmlFor="age" className="col-4 col-form-label">
                        Age
                      </label>
                      <div className="col-8">
                        <input
                          id="age"
                          name="age"
                          placeholder="Age"
                          className="form-control here"
                          type="number"
                          min={0}
                          max={120}
                          value={user.age}
                          onChange={onChange}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label htmlFor="bio" className="col-4 col-form-label">
                        Bio
                      </label>
                      <div className="col-8">
                        <textarea
                          id="bio"
                          name="bio"
                          placeholder="Bio"
                          cols="40"
                          rows="4"
                          className="form-control"
                          value={user.bio}
                          onChange={onChange}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <label htmlFor="workPlace" className="col-4 col-form-label">
                        Work place
                      </label>
                      <div className="col-8">
                        <input
                          id="workPlace"
                          name="workPlace"
                          placeholder="Work place"
                          className="form-control here"
                          type="text"
                          value={user.workPlace}
                          onChange={onChange}
                        />
                      </div>
                    </div>
                    <div className="form-group row">
                      <div className="offset-4 col-8">
                        <button name="submit" type="submit" className="btn btn-pink">
                          Update My Profile
                        </button>
                      </div>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
