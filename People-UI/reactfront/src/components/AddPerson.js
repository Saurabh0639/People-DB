import React from "react";

class AddPerson extends React.Component {
  state = {
    firstName: "",
    lastName: "",
    age: ""
  };

  add = (e) => {
    e.preventDefault();
    if (this.state.firstName === "" || this.state.lastName === "" || this.state.age === "") {
      alert("ALL the fields are mandatory!");
      return;
    }
    this.props.addPersonHandler(this.state);
    this.setState({ firstName: "", lastName: "", age: "" });
    this.props.history.push("/");
  };
  render() {
    return (
      <div className="ui main">
        <h2>New Person</h2>
        <form className="ui form" onSubmit={this.add}>
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
          <button className="ui blue button">Add</button>
          {/* <button className="ui button blue">Add</button> */}
        </form>
      </div>
    );
  }
}

export default AddPerson;
