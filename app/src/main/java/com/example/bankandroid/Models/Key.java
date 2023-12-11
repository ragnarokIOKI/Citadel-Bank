package com.example.bankandroid.Models;

public class Key {
    private String access_token;
    private String username;
    private int id;

    public Key(String access_token, String username, int id) {
        this.access_token = access_token;
        this.username = username;
        this.id = id;
    }

    public Key() {
    }

    public String getAccess_token() {
        return access_token;
    }

    public void setAccess_token(String access_token) {
        this.access_token = access_token;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
