﻿import React, { Component } from 'react';
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { Redirect } from "react-router-dom"; 

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = { isSuccess: Boolean, userName: String, password: String }
        this.loginOnClick = this.loginOnClick.bind(this); 
    }

    loginOnClick(event) {
        event.preventDefault();
        this.sendLoginRequest();
    }


    sendLoginRequest() {
        let loginModel = {
            Username: this.state.userName.trim(),
            Password: this.state.password
        }
        const requestOptions = {
            method: 'POST',
            headers: {
                'Accept': 'application/json, application/xml, text/plain, text/html, *.*',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginModel)
        };
         
        fetch('api/user/login', requestOptions)
            .then(response => {
                if (response.ok && response.status >= 200 && response.status < 300) {
                    this.setState({ isSuccess: true });
                } else {
                    alert("Check your information and try again!!!");
                }
            });
    }
 
    
    render() {
        if (this.state.isSuccess === true) {
            return <Redirect to="/monitor-data" />;
        }
        return (
            <div class="login-form">
                <img class="logo" src={require('../img/CCCLogo.jpg')} /> 
                <Form onSubmit={event => this.loginOnClick(event)}>
                    <Form.Group controlId="formBasicUserName">
                        <Form.Label>User Name</Form.Label>
                        <Form.Control type="text" placeholder="Enter username" onChange={event => this.setState({ userName: event.target.value })} />
                    </Form.Group>

                    <Form.Group controlId="formBasicPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control type="password" placeholder="Password" onChange={event => this.setState({ password: event.target.value })} />
                        <Form.Text className="text-muted">
                            We'll never share your password with anyone else.
                   </Form.Text>
                    </Form.Group>
                    <Button variant="primary" type="submit">
                        Log In
                </Button>
                </Form>
            </div>
        );
    }
}
