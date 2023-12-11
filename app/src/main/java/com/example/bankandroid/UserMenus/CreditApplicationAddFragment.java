package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.app.Dialog;
import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.bankandroid.Models.AccountType;
import com.example.bankandroid.Models.CreditApplication;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;
import com.google.android.material.textfield.TextInputLayout;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreditApplicationAddFragment extends DialogFragment {
    Button cancel, add;
    ApiInterface Interface;
    EditText editTextAmountCredit, editTextDesiredPercentageCredit;
    int idUs;
    boolean isValid;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_credit_application_add, container, false);


        cancel = root.findViewById(R.id.buttonCancelCredit);
        add = root.findViewById(R.id.buttonAddCredit);
        editTextDesiredPercentageCredit = root.findViewById(R.id.editTextDesiredPercentageCredit);
        editTextAmountCredit = root.findViewById(R.id.editTextAmountCredit);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        idUs = sharedPreferences.getInt("id", 0);

        SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss", Locale.getDefault());

        editTextDesiredPercentageCredit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                String text = s.toString();
                if (text.isEmpty()) {
                    editTextDesiredPercentageCredit.setError("Поле не может быть пустым.");
                    isValid = false;
                } else if (text.contains(",")) {
                    editTextDesiredPercentageCredit.setError("Используйте точку вместо запятой для дробных чисел.");
                    isValid = false;
                } else {
                    editTextDesiredPercentageCredit.setError(null);
                    isValid = true;
                }
            }
        });

        editTextAmountCredit.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                String text = s.toString();
                if (text.isEmpty()) {
                    editTextAmountCredit.setError("Поле не может быть пустым.");
                    isValid = false;
                } else if (text.contains(",")) {
                    editTextAmountCredit.setError("Используйте точку вместо запятой для дробных чисел.");
                    isValid = false;
                } else {
                    editTextAmountCredit.setError(null);
                    isValid = true;
                }
            }
        });

        add.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(isValid == true){
                    Call<CreditApplication> post = Interface.postCreditApplication(new CreditApplication(String.valueOf(sdf.format(new Date())), Double.valueOf(editTextAmountCredit.getText().toString()),
                            Double.valueOf(editTextDesiredPercentageCredit.getText().toString()), idUs, 1));
                    post.enqueue(new Callback<CreditApplication>() {
                        @Override
                        public void onResponse(Call<CreditApplication> call, Response<CreditApplication> response) {
                            if(response.isSuccessful()){
                                Toast.makeText(getContext(), "Успешно добавлено", Toast.LENGTH_SHORT).show();
                                editTextDesiredPercentageCredit.setText("");
                                editTextAmountCredit.setText("");
                            }
                        }

                        @Override
                        public void onFailure(Call<CreditApplication> call, Throwable t) {
                            Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                        }
                    });
                } else {
                    Toast.makeText(getContext(), "Ошибка заполнения формы", Toast.LENGTH_SHORT).show();
                }
            }
        });

        cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });

        return root;
    }

    @Override
    public void onStart() {
        super.onStart();
        Dialog dialog = getDialog();
        if (dialog != null) {
            int width = ViewGroup.LayoutParams.MATCH_PARENT;
            int height = ViewGroup.LayoutParams.WRAP_CONTENT;
            dialog.getWindow().setLayout(width, height);
        }
    }
}