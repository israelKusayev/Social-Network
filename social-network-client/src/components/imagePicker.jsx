import React, { Component } from 'react';

export default class ImagePicker extends Component {
  state = {
    file: [],
    error: ''
  };

  onChange = () => {
    var file = this.refs.file.files[0];
    var fileType = file['type'];
    var ValidImageTypes = ['image/gif', 'image/jpeg', 'image/png'];
    if (!ValidImageTypes.includes(fileType)) {
      this.setState({ error: 'You can opload only photos' });
      this.props.onUpload('');
      return;
    }

    var reader = new FileReader();

    reader.readAsDataURL(file);
    reader.onloadend = (e) => {
      this.props.onUpload(reader.result);

      this.setState({
        error: ''
      });
    };
  };

  render() {
    return (
      <div>
        <div className="custom-file">
          <input ref="file" type="file" accept="jpg" id="customFile" onChange={this.onChange} />
          <label className="custom-file-label" htmlFor="customFile">
            Upload image
          </label>
        </div>

        <img className="card-img-bottom mt-4" src={this.props.image} alt="" />
        {this.state.error && <div className="alert alert-danger">{this.state.error} </div>}
      </div>
    );
  }
}
