package com.example.bankandroid;

import static android.content.Context.MODE_PRIVATE;

import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import com.example.bankandroid.Models.Key;
import com.example.bankandroid.Models.LogInModel;
import com.example.bankandroid.Models.User;
import com.example.bankandroid.UserMenus.UserFragment;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;
import com.example.bankandroid.Utility.LogInHelper;
import com.google.android.material.textfield.TextInputLayout;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SettingsFragment extends Fragment {
    Button save, exit;
    ImageView down;
    int progress = 15;
    ArrayList<LogInModel> logArr;
    LogInModel log;
    int idUs;
    LogInHelper logInHelper;
    EditText FIOedit, loginedit, seredit, numedit, birthedit;
    TextInputLayout FIOlayout, loginlayout, serlayout, numlayout, birthlayout;
    boolean isValid = false;
    String fio, firstName, lastName, middleName;
    String[] nameParts;
    ApiInterface Interface;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_settings, container, false);
        save = root.findViewById(R.id.savesettings);
        down = root.findViewById(R.id.downSet);
        exit = root.findViewById(R.id.exitsystem);
        FIOedit = root.findViewById(R.id.FIOedit);
        loginedit = root.findViewById(R.id.loginedit);
        seredit = root.findViewById(R.id.PassportSeredit);
        numedit = root.findViewById(R.id.PassportNumedit);
        birthedit = root.findViewById(R.id.Birthedit);
        FIOlayout = root.findViewById(R.id.FIOlayout);
        loginlayout = root.findViewById(R.id.loginLayout);
        serlayout = root.findViewById(R.id.PassportSerlayout);
        numlayout = root.findViewById(R.id.PassportNumlayout);
        birthlayout = root.findViewById(R.id.Birthlayout);

        logInHelper = new LogInHelper(getContext());

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        idUs = sharedPreferences.getInt("id", 0);

        birthedit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDatePickerDialog();
            }
        });

        exit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                AlertDialog.Builder builder = new AlertDialog.Builder(getContext());
                builder.setTitle("Цитадель-Банк");
                builder.setMessage("Вы уверены, что хотите выйти из системы?")
                        .setPositiveButton("Выйти", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                logInHelper.removeAll();
                                Intent i = new Intent(getContext(), LogIn.class);
                                startActivity(i);
                            }
                        })
                        .setNegativeButton("Отмена", new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.dismiss();
                            }
                        });
                AlertDialog dialog = builder.create();
                dialog.show();
            }
        });

        down.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                UserFragment newFragment = new UserFragment();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                fragmentTransaction.setCustomAnimations( R.anim.slidedown,0);
                fragmentTransaction.replace(R.id.fragment_layout, newFragment);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();
            }
        });

        Call<User> us = Interface.getUserBy(idUs);
        us.enqueue(new Callback<User>() {
            @Override
            public void onResponse(Call<User> call, Response<User> response) {
                FIOedit.setText(response.body().getFirstName() + " " + response.body().getSecondName() + " " + response.body().getMiddleName());
                loginedit.setText(response.body().getLogin());
                seredit.setText(response.body().getPassportSeries());
                numedit.setText(response.body().getPassportNumber());
                birthedit.setText(response.body().getBirthday());
            }

            @Override
            public void onFailure(Call<User> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

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

        save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (isValid == true) {
                    Call<User> put = Interface.putUser(idUs, new User(idUs, firstName, lastName, middleName, birthedit.getText().toString(),seredit.getText().toString(),
                            numedit.getText().toString(), loginedit.getText().toString(), logInHelper.getPassword(), "", 2, true));
                    put.enqueue(new Callback<User>() {
                        @Override
                        public void onResponse(Call<User> call, Response<User> response) {
                            Toast.makeText(getContext(), "Успешно", Toast.LENGTH_SHORT).show();

                        }

                        @Override
                        public void onFailure(Call<User> call, Throwable t) {
                            Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                        }
                    });
                }
            }
        });

        return root;
    }
    private void showDatePickerDialog() {
        Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        DatePickerDialog datePickerDialog = new DatePickerDialog(getContext(), new DatePickerDialog.OnDateSetListener() {
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