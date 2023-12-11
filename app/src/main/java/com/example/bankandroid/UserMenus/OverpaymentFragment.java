package com.example.bankandroid.UserMenus;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
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
    EditText editTextAmountCredit, editTextPercentage, editTextDateTermination, editTextDateDrawing;
    Button exit, calculate;
    ApiInterface Interface;

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

        exit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });

        calculate.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Call<OverpaymentDTO> getOverpay = Interface.getOverpayment(Double.valueOf(editTextAmountCredit.getText().toString()),
                        Double.valueOf(editTextPercentage.getText().toString()),
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