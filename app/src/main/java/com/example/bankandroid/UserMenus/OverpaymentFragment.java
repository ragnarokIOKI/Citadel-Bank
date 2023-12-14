package com.example.bankandroid.UserMenus;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.widget.*;
import androidx.fragment.app.DialogFragment;
import androidx.fragment.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import com.example.bankandroid.Models.Account;
import com.example.bankandroid.Models.CreditAgreement;
import com.example.bankandroid.Models.OverpaymentDTO;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;
import com.google.android.material.textfield.TextInputEditText;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

public class OverpaymentFragment extends DialogFragment {

    TextView resultOverpayment;
    TextInputEditText editTextAmountCredit, editTextPercentage, editTextDateTermination, editTextDateDrawing;
    Button exit, calculate;
    ApiInterface Interface;
    boolean isValid = false;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_overpayment, container, false);

        resultOverpayment = root.findViewById(R.id.resultOverpayment);
        editTextAmountCredit = root.findViewById(R.id.editTextAmountCredit);
        editTextDateDrawing = root.findViewById(R.id.editTextDateDrawing);
        editTextDateTermination = root.findViewById(R.id.editTextDateTermination);
        editTextPercentage = root.findViewById(R.id.editTextPercentage);
        calculate = root.findViewById(R.id.buttonAdd);
        exit = root.findViewById(R.id.buttonCancel);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        editTextDateTermination.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDatePickerDialog1();
            }
        });

        editTextDateDrawing.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDatePickerDialog2();
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
                if (s.toString().isEmpty()) {
                    editTextAmountCredit.setError("Поле не может быть пустым.");
                    isValid = false;
                } else {
                    editTextAmountCredit.setError(null);
                    isValid = true;
                }
            }
        });

        editTextPercentage.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if (s.toString().isEmpty()) {
                    editTextAmountCredit.setError("Поле не может быть пустым.");
                    isValid = false;
                } else {
                    editTextAmountCredit.setError(null);
                    isValid = true;
                }
            }
        });

        exit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });

        calculate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (isValid){
                    Call<OverpaymentDTO> getOverpay = Interface.getOverpayment(Double.parseDouble(editTextAmountCredit.getText().toString()),
                            Double.parseDouble(editTextPercentage.getText().toString()),
                            editTextDateDrawing.getText().toString(), editTextDateTermination.getText().toString());
                    getOverpay.enqueue(new Callback<OverpaymentDTO>() {
                        @Override
                        public void onResponse(Call<OverpaymentDTO> call, Response<OverpaymentDTO> response) {
                            assert response.body() != null;
                            resultOverpayment.setText(String.valueOf(response.body().getOverpayment()));
                        }

                        @Override
                        public void onFailure(Call<OverpaymentDTO> call, Throwable t) {
                            Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                        }
                    });
                } else {
                    Toast.makeText(getContext(), "Заполните поля!", Toast.LENGTH_SHORT).show();
                }
            }
        });

        return root;
    }
    private void showDatePickerDialog1() {
        Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        DatePickerDialog datePickerDialog = new DatePickerDialog(
                getContext(), new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd", Locale.getDefault());
                Calendar date = Calendar.getInstance();
                date.set(year, month, dayOfMonth);
                String selectedDate = sdf.format(date.getTime());
                editTextDateTermination.setText(selectedDate);
            }
        }, year, month, day);

        datePickerDialog.show();
    }
    private void showDatePickerDialog2() {
        Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        DatePickerDialog datePickerDialog = new DatePickerDialog(
                getContext(), new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker view, int year, int month, int dayOfMonth) {
                SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd", Locale.getDefault());
                Calendar date = Calendar.getInstance();
                date.set(year, month, dayOfMonth);
                String selectedDate = sdf.format(date.getTime());
                editTextDateDrawing.setText(selectedDate);
            }
        }, year, month, day);

        datePickerDialog.show();
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