package com.example.bankandroid.Utility;

import android.content.Context;
import android.content.SharedPreferences;

public class LogInHelper {
    private static final String PREF_NAME = "loginPrefs";
    private static final String KEY_LOGIN = "login";
    private static final String KEY_PASSWORD = "password";
    private static final String KEY_SALT = "salt";

    private SharedPreferences sharedPreferences;

    public LogInHelper(Context context) {
        sharedPreferences = context.getSharedPreferences(PREF_NAME, Context.MODE_PRIVATE);
    }

    public void saveLogin(String login) {
        sharedPreferences.edit().putString(KEY_LOGIN, login).apply();
    }

    public void savePassword(String password) {
        sharedPreferences.edit().putString(KEY_PASSWORD, password).apply();
    }

    public void saveSalt(String salt) {
        sharedPreferences.edit().putString(KEY_SALT, salt).apply();
    }

    public String getLogin() {
        return sharedPreferences.getString(KEY_LOGIN, "");
    }

    public String getPassword() {
        return sharedPreferences.getString(KEY_PASSWORD, "");
    }
    public String getSalt() {
        return sharedPreferences.getString(KEY_SALT, "");
    }

    public void removeLogin() {
        sharedPreferences.edit().remove(KEY_LOGIN).apply();
    }

    public void removePassword() {
        sharedPreferences.edit().remove(KEY_PASSWORD).apply();
    }

    public void removeAll() {
        sharedPreferences.edit().clear().apply();
    }
}