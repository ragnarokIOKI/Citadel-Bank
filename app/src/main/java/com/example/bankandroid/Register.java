package com.example.bankandroid;

import androidx.appcompat.app.AppCompatActivity;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.Toast;

import com.example.bankandroid.Models.Key;
import com.example.bankandroid.Models.User;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;
import com.google.android.material.textfield.TextInputLayout;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class Register extends AppCompatActivity {
    ApiInterface Interface;
    Button reg, logIn;
    EditText passedit, repeatpassedit, FIOedit, loginedit, seredit, numedit, birthedit;
    TextInputLayout passeditlayout, repeatpasslayout, FIOlayout, loginlayout, serlayout, numlayout, birthlayout;
    boolean isValid = true;
    String fio, firstName, lastName, middleName;
    String[] nameParts;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        reg = findViewById(R.id.auth);
        logIn = findViewById(R.id.logEnter);
        passedit = findViewById(R.id.passwordedit);
        repeatpassedit = findViewById(R.id.repeatpassword);
        FIOedit = findViewById(R.id.FIOedit);
        loginedit = findViewById(R.id.loginedit);
        seredit = findViewById(R.id.PassportSeredit);
        numedit = findViewById(R.id.PassportNumedit);
        birthedit = findViewById(R.id.Birthedit);
        passeditlayout = findViewById(R.id.passwordLayout);
        repeatpasslayout = findViewById(R.id.repeatLayout);
        FIOlayout = findViewById(R.id.FIOlayout);
        loginlayout = findViewById(R.id.loginLayout);
        serlayout = findViewById(R.id.PassportSerlayout);
        numlayout = findViewById(R.id.PassportNumlayout);
        birthlayout = findViewById(R.id.Birthlayout);

        Interface = Configurator.buildRequest().create(ApiInterface.class);
        SharedPreferences sharedPreferences = getSharedPreferences("myPrefs", MODE_PRIVATE);
        String token = sharedPreferences.getString("token", "");
        int usId = sharedPreferences.getInt("id", 0);

        FIOedit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(FIOedit.getText().toString().isEmpty()){
                    FIOedit.setError("Поле не может быть пустым.");
                    isValid = false;
                }else {
                    FIOlayout.setErrorEnabled(false);
                    isValid = true;
                    fio = FIOedit.getText().toString();
                    nameParts = fio.split(" ", 3);

                    if (nameParts.length == 1) {
                        FIOedit.setError("Минимальное требование - Фамилия и Имя.");
                        isValid = false;
                    } else if (nameParts.length == 2) {
                        lastName = nameParts[0];
                        firstName = nameParts[1];
                        middleName = "";
                        isValid = true;
                    } else {
                        lastName = nameParts[0];
                        firstName = nameParts[1];
                        middleName = nameParts[2];
                        isValid = true;
                    }
                }
            }
        });

        seredit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(seredit.getText().toString().isEmpty()){
                    seredit.setError("Поле не может быть пустым.");
                    isValid = false;
                }else {
                    serlayout.setErrorEnabled(false);
                    isValid = true;
                }
            }
        });

        numedit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(numedit.getText().toString().isEmpty()){
                    numedit.setError("Поле не может быть пустым.");
                    isValid = false;
                }else {
                    numlayout.setErrorEnabled(false);
                    isValid = true;
                }
            }
        });

        birthedit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(birthedit.getText().toString().isEmpty()){
                    birthedit.setError("Поле не может быть пустым.");
                    isValid = false;
                }else {
                    birthlayout.setErrorEnabled(false);
                    isValid = true;
                }
            }
        });

        loginedit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                Pattern pat;
                Matcher mat;

                final String LOGIN_PATTERN = "^[a-zA-Z0-9_-]{3,15}$";
                pat = Pattern.compile(LOGIN_PATTERN);
                mat = pat.matcher(loginedit.getText().toString());
                if(mat.matches()){
                    if (loginedit.getText().length() < 3 || loginedit.getText().length() > 15) {
                        loginedit.setError("Не менее 3 символов и не более 15.");
                        isValid = false;
                    } else {
                        loginlayout.setErrorEnabled(false);
                        isValid = true;
                    }
                } else {
                    loginedit.setError("Поле не соответствует валидации.");
                    isValid = false;
                    if (loginedit.getText().toString().isEmpty()) {
                        loginedit.setError("Поле не может быть пустым.");
                        isValid = false;
                    } else {
                        loginlayout.setErrorEnabled(false);
                        isValid = true;
                    }
                }
            }
        });

        passedit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }
            @Override
            public void afterTextChanged(Editable s) {
                Pattern pattern;
                Matcher matcher;

                final String PASSWORD_PATTERN = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{6,16}$";

                pattern = Pattern.compile(PASSWORD_PATTERN);
                matcher = pattern.matcher(passedit.getText().toString());
                if (matcher.matches())
                {
                    if (passedit.length() < 6) {
                        passedit.setError("Пароль не может быть менее 6 символов.");
                        isValid = false;
                    } else{
                        passeditlayout.setErrorEnabled(false);
                        isValid = true;
                    }
                    passeditlayout.setErrorEnabled(false);
                    isValid = true;
                } else {
                    passedit.setError("Поле не соответствует валидации.");
                    isValid = false;
                    if(passedit.getText().toString().isEmpty()){
                        passedit.setError("Поле не может быть пустым.");
                        isValid = false;
                    }else {
                        passeditlayout.setErrorEnabled(false);
                        isValid = true;
                    }
                }
            }
        });

        repeatpassedit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }
            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }
            @Override
            public void afterTextChanged(Editable s) {
                Pattern pattern;
                Matcher matcher;

                final String PASSWORD_PATTERN = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{6,16}$";

                pattern = Pattern.compile(PASSWORD_PATTERN);
                matcher = pattern.matcher(repeatpassedit.getText().toString());
                if (matcher.matches())
                {
                    if (repeatpassedit.length() < 6) {
                        repeatpassedit.setError("Пароль не может быть менее 6 символов.");
                        isValid = false;
                    } else{
                        repeatpasslayout.setErrorEnabled(false);
                        isValid = true;
                    }
                    if (repeatpassedit.getText().toString().equals(repeatpassedit.getText().toString())){
                        repeatpasslayout.setErrorEnabled(false);
                        isValid = true;
                    } else{
                        repeatpassedit.setError("Поля не совпадают.");
                        isValid = false;
                    }
                } else {
                    repeatpassedit.setError("Поле не соответствует валидации.");
                    isValid = false;
                    if(repeatpassedit.getText().toString().isEmpty()){
                        repeatpassedit.setError("Поле не может быть пустым.");
                        isValid = false;
                    }else {
                        repeatpasslayout.setErrorEnabled(false);
                        isValid = true;
                    }
                }
            }
        });

        birthedit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDatePickerDialog();
            }
        });

        logIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(getApplicationContext(), LogIn.class);
                startActivity(i);
                overridePendingTransition( 0, R.anim.slidein);
                finish();
            }
        });

        reg.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
               if (isValid == true) {
                   Call<User> add = Interface.auth(new User(firstName, lastName, middleName, birthedit.getText().toString(), seredit.getText().toString(),
                           numedit.getText().toString(), loginedit.getText().toString(), passedit.getText().toString(), "", 2, true));
                   add.enqueue(new Callback<User>() {
                       @Override
                       public void onResponse(Call<User> call, Response<User> response) {
                           if (response.isSuccessful()){
                               Call<Key> login = Interface.login(loginedit.getText().toString(), passedit.getText().toString());
                               login.enqueue(new Callback<Key>() {
                                   @Override
                                   public void onResponse(Call<Key> call, Response<Key> response) {
                                       int ke = response.body().getId();
                                       String token = response.body().getAccess_token();
                                       Toast.makeText(getApplicationContext(), "Успешная регистрация", Toast.LENGTH_SHORT).show();
                                       Intent intent = new Intent(getApplicationContext(), MainActivity.class).putExtra("id", ke);
                                       startActivity(intent);
                                   }

                                   @Override
                                   public void onFailure(Call<Key> call, Throwable t) {

                                   }
                               });
                           }
                       }
                       @Override
                       public void onFailure(Call<User> call, Throwable t) {
                           Toast.makeText(getApplicationContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                       }
                   });
               }
            }
        });
    }

    private void showDatePickerDialog() {
        Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        DatePickerDialog datePickerDialog = new DatePickerDialog(
                this, new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd", Locale.getDefault());
                Calendar date = Calendar.getInstance();
                date.set(year, month, dayOfMonth);
                String selectedDate = sdf.format(date.getTime());
                birthedit.setText(selectedDate);
            }
        }, year, month, day);

        datePickerDialog.show();
    }
}