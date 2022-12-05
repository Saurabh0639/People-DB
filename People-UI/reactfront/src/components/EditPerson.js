import React from "react";

class EditPerson extends React.Component {
  constructor(props) {
    super(props);
    const { id, firstName, lastName, age } = props.location.state.person;
    this.state = {
      id,
      firstName,
      lastName,
      age
    };
  }

  update = (e) => {
    e.preventDefault();
    if (this.state.firstName === "" || this.state.lastName === ""|| this.state.age === "") {
      alert("ALl the fields are mandatory!");
      return;
    }
    this.props.updatePersonHandler(this.state);
    this.setState({ firstName: "", lastName: "", age: "" });
    this.props.history.push("/");
    
  };
  render() {
    return (
      <div className="ui main">
        <h2>Edit Person</h2>
        <form className="ui form" onSubmit={this.update}>
        <div className="field">
            <label>First Name</label>
            <input
              type="text"
              name="firstName"
              placeholder="First Name"
              value={this.state.firstName}
              onChange={(e) => this.setState({ firstName: e.target.value })}
            />
          </div>
          <div className="field">
            <label>Last Name</label>
            <input
              type="text"
              name="lastName"
              placeholder="Last Name"
              value={this.state.lastName}
              onChange={(e) => this.setState({ lastName: e.target.value })}
            />
          </div>
          <div className="field">
            <label>Age</label>
            <input
              type="text"
              name="age"
              placeholder="Age"
              value={this.state.age}
              onChange={(e) => this.setState({ age: e.target.value })}
            />
          </div>
          <button className="ui button blue">Update</button>
        </form>
      </div>
    );
  }
}

export default EditPerson;
