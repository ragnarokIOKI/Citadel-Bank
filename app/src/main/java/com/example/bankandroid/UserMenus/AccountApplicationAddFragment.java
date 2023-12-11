package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.app.Dialog;
import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.fragment.app.DialogFragment;

import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.bankandroid.Models.AccountApplication;
import com.example.bankandroid.Models.AccountType;
import com.example.bankandroid.Models.CreditApplication;
import com.example.bankandroid.R;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Класс AccountApplicationAddFragment предназначен для создания заявок на открытие счета.
 * Он включает в себя пользовательский интерфейс для ввода информации о новом счете и его создание.
 */
public class AccountApplicationAddFragment extends DialogFragment {
    Spinner sTypeAccountId;
    ApiInterface Interface;
    ArrayList<String> rray;
    Button cancel, add;
    EditText editTextAccountDesiredPercentage;
    CheckBox checkBoxBankCardNeeded;
    boolean isValid = true;
    int idUs;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View root = inflater.inflate(R.layout.fragment_account_application_add, container, false);

        sTypeAccountId = root.findViewById(R.id.sTypeAccountId);
        cancel = root.findViewById(R.id.buttonCancel);
        add = root.findViewById(R.id.buttonAdd);
        editTextAccountDesiredPercentage = root.findViewById(R.id.editTextAccountDesiredPercentage);
        checkBoxBankCardNeeded = root.findViewById(R.id.checkBoxBankCardNeeded);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        idUs = sharedPreferences.getInt("id", 0);

        rray = new ArrayList<String>();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyyMMddHHmmss", Locale.getDefault());

        cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dismiss();
            }
        });

        editTextAccountDesiredPercentage.addTextChangedListener(new TextWatcher() {
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
                    editTextAccountDesiredPercentage.setError("Поле не может быть пустым.");
                    isValid = false;
                } else if (text.contains(",")) {
                    editTextAccountDesiredPercentage.setError("Используйте точку вместо запятой для дробных чисел.");
                    isValid = false;
                } else {
                    editTextAccountDesiredPercentage.setError(null);
                    isValid = true;
                }
            }
        });

        Call<ArrayList<AccountType>> getAccountType = Interface.getAccountType();
        getAccountType.enqueue(new Callback<ArrayList<AccountType>>() {
            @Override
            public void onResponse(Call<ArrayList<AccountType>> call, Response<ArrayList<AccountType>> response) {
                if (response.isSuccessful()){
                    for (int i = response.body().size() - 1; i >= 0; i--){
                        rray.add(response.body().get(i).getNameAccountType());
                    }

                    ArrayAdapter<String> adapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, rray);
                    adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sTypeAccountId.setAdapter(adapter);

                }else {
                    Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<ArrayList<AccountType>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        add.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (isValid == true){
                    AccountApplication a = new AccountApplication(sdf.format(new Date()),  sTypeAccountId.getSelectedItemPosition(), Double.valueOf(editTextAccountDesiredPercentage.getText().toString()), idUs, 1, checkBoxBankCardNeeded.isChecked());
                    Call<AccountApplication> post = Interface.postAccountApplication(a);
                    post.enqueue(new Callback<AccountApplication>() {
                        @Override
                        public void onResponse(Call<AccountApplication> call, Response<AccountApplication> response) {
                            Toast.makeText(getContext(), "Успешно добавлено", Toast.LENGTH_SHORT).show();
                            editTextAccountDesiredPercentage.setText("");
                            checkBoxBankCardNeeded.setChecked(false);
                        }

                        @Override
                        public void onFailure(Call<AccountApplication> call, Throwable t) {
                            Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                        }
                    });
                }
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
