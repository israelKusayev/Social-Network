import React, { Component } from 'react';

export default class ImagePicker extends Component {
  state = {
    file: [],
    imgSrc: ''
  };

  onChange = () => {
    // Assuming only image
    var file = this.refs.file.files[0];
    var reader = new FileReader();

    reader.readAsDataURL(file);

    reader.onloadend = (e) => {
      this.setState({
        imgSrc: [reader.result]
      });
    };

    console.log(reader);
    // TODO: concat files
  };

  render() {
    return (
      <div>
        <div className="custom-file">
          <input ref="file" type="file" id="customFile" onChange={this.onChange} />
          <label className="custom-file-label" for="customFile">
            Upload image
          </label>
        </div>
        <div className="py-2" />

        <img className="card-img-bottom" src={this.state.imgSrc} alt="" />
      </div>
    );
  }
}
