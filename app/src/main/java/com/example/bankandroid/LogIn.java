package com.example.bankandroid;

import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;

import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.bankandroid.Models.Key;
import com.example.bankandroid.Models.LogInModel;
import com.example.bankandroid.UserMenus.CardAccountFragment;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;
import com.example.bankandroid.Utility.LogInHelper;
import com.google.android.material.textfield.TextInputLayout;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LogIn extends AppCompatActivity {
    Button Enter, regEnter;
    EditText editlog, password;
    TextInputLayout loginLOGlayout, loginPASSWORDlayout;
    boolean isValid = true;
    ApiInterface Interface;
    ArrayList<LogInModel> logArr;
    LogInHelper logInHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_log_in);
        setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        Enter = findViewById(R.id.enter);
        editlog = findViewById(R.id.loginLOG);
        password = findViewById(R.id.loginPASSWORD);
        loginLOGlayout = findViewById(R.id.loginEMAILlayout);
        loginPASSWORDlayout = findViewById(R.id.loginPASSWORDlayout);
        regEnter = findViewById(R.id.regEnter);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        logInHelper = new LogInHelper(this);

        String savedLogin = logInHelper.getLogin();
        String savedPassword = logInHelper.getPassword();

        if (!savedLogin.isEmpty() && !savedPassword.isEmpty()) {
            editlog.setText(savedLogin);
            password.setText(savedPassword);
        }

        regEnter.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(getApplicationContext(), Register.class);
                startActivity(intent);
                overridePendingTransition(R.anim.slideout, R.anim.fadeout);
                finish();
            }
        });

        editlog.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }
            @Override
            public void afterTextChanged(Editable s) {
                if(editlog.getText().toString().isEmpty()){
                    editlog.setError("Поле не может быть пустым.");
                    isValid = false;
                }else {
                    loginLOGlayout.setErrorEnabled(false);
                    isValid = true;
                }
            }
        });

        password.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }
            @Override
            public void afterTextChanged(Editable s) {
                if(password.getText().toString().isEmpty()){
                    password.setError("Поле не может быть пустым.");
                    isValid = false;
                }else {
                    loginPASSWORDlayout.setErrorEnabled(false);
                    isValid = true;
                }
            }
        });



        Enter.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                //startActivity(intent);
                if (editlog.getText().toString().isEmpty()) {
                    editlog.setError("Поле не может быть пустым.");
                    isValid = false;
                } else {
                    if (password.getText().toString().isEmpty()) {
                        password.setError("Поле не может быть пустым.");
                        isValid = false;
                    } else {
                        if (isValid) {
                            Call<Key> login = Interface.login(editlog.getText().toString(), password.getText().toString());
                            login.enqueue(new Callback<Key>() {
                                @Override
                                public void onResponse(Call<Key> call, Response<Key> response) {
                                    if (response.isSuccessful()) {
                                        int ke = response.body().getId();
                                        String token = response.body().getAccess_token();
                                        Fragment usFrag = new CardAccountFragment(ke);
                                        SharedPreferences sharedPreferences = getSharedPreferences("myPrefs", MODE_PRIVATE);
                                        SharedPreferences.Editor editor = sharedPreferences.edit();
                                        editor.putString("token", response.body().getAccess_token());
                                        editor.putInt("id", response.body().getId());
                                        editor.apply();
                                        LogInModel log = new LogInModel(editlog.getText().toString(), password.getText().toString());
                                        logInHelper.saveLogin(editlog.getText().toString());
                                        logInHelper.savePassword(password.getText().toString());
                                        Intent intent = new Intent(getApplicationContext(), MainActivity.class).putExtra("id", ke);
                                        startActivity(intent);
                                    } else {
                                        Toast.makeText(getApplicationContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                                    }
                                }

                                @Override
                                public void onFailure(Call<Key> call, Throwable t) {
                                    Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                                }
                            });
                        }
                    }
                }
            }
        });
    }
}