import React from 'react';

export default function FollowerTab(props) {
  return (
    <>
      <div className="follow row">
        <div className="col-md-6">
          <span className="font-size-bigger align-middle">{props.name}</span>
        </div>
        <div className="col-md-6">
          <span className="float-right">
            {props.leftBtnName && (
              <button type="button" onClick={props.onLeftBtnClicked} className=" mr-2 btn btn-dark btn-followTab">
                {props.leftBtnName}
              </button>
            )}
            {props.rightBtnName && (
              <button type="button" onClick={props.onRightBtnClicked} className="btn btn-dark ml-4 btn-followTab">
                {props.rightBtnName}
              </button>
            )}
          </span>
        </div>
      </div>
    </>
  );
}
