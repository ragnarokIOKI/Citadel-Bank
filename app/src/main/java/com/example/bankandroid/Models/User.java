package com.example.bankandroid.Models;

import com.google.gson.annotations.SerializedName;

public class User {
    @SerializedName("idUser")
    private int IdUser;
    @SerializedName("firstName")
    private String FirstName;
    @SerializedName("secondName")
    private String SecondName;
    @SerializedName("middleName")
    private String MiddleName;
    @SerializedName("birthday")
    private String Birthday;
    @SerializedName("passportSeries")
    private String PassportSeries;
    @SerializedName("passportNumber")
    private String PassportNumber;
    @SerializedName("login")
    private String Login;
    @SerializedName("password")
    private String Password;
    @SerializedName("salt")
    private String Salt;
    @SerializedName("roleId")
    private int RoleId;
    @SerializedName("userDeleted")
    private boolean Deleted;

    public User() {
    }

    public User(String login, String password) {
        Login = login;
        Password = password;
    }

    public User(int idUser, String firstName, String secondName, String middleName, String birthday, String passportSeries, String passportNumber, String login, String password, String salt, int roleId, boolean deleted) {
        IdUser = idUser;
        FirstName = firstName;
        SecondName = secondName;
        MiddleName = middleName;
        Birthday = birthday;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        Login = login;
        Password = password;
        Salt = salt;
        RoleId = roleId;
        Deleted = deleted;
    }

    public User(String firstName, String secondName, String middleName, String birthday, String passportSeries, String passportNumber, String login, String password, String salt, int roleId, boolean deleted) {
        FirstName = firstName;
        SecondName = secondName;
        MiddleName = middleName;
        Birthday = birthday;
        PassportSeries = passportSeries;
        PassportNumber = passportNumber;
        Login = login;
        Password = password;
        Salt = salt;
        RoleId = roleId;
        Deleted = deleted;
    }

    public int getIdUser() {
        return IdUser;
    }

    public void setIdUser(int idUser) {
        IdUser = idUser;
    }

    public String getFirstName() {
        return FirstName;
    }

    public void setFirstName(String firstName) {
        FirstName = firstName;
    }

    public String getSecondName() {
        return SecondName;
    }

    public void setSecondName(String secondName) {
        SecondName = secondName;
    }

    public String getMiddleName() {
        return MiddleName;
    }

    public void setMiddleName(String middleName) {
        MiddleName = middleName;
    }

    public String getBirthday() {
        return Birthday;
    }

    public void setBirthday(String birthday) {
        Birthday = birthday;
    }

    public String getPassportSeries() {
        return PassportSeries;
    }

    public void setPassportSeries(String passportSeries) {
        PassportSeries = passportSeries;
    }

    public String getPassportNumber() {
        return PassportNumber;
    }

    public void setPassportNumber(String passportNumber) {
        PassportNumber = passportNumber;
    }

    public String getLogin() {
        return Login;
    }

    public void setLogin(String login) {
        Login = login;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getSalt() {
        return Salt;
    }

    public void setSalt(String salt) {
        Salt = salt;
    }

    public int getRoleId() {
        return RoleId;
    }

    public void setRoleId(int roleId) {
        RoleId = roleId;
    }

    public boolean isDeleted() {
        return Deleted;
    }

    public void setDeleted(boolean deleted) {
        Deleted = deleted;
    }
}
