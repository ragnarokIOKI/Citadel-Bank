package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.content.SharedPreferences;
import android.nfc.Tag;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.example.bankandroid.Models.AccountApplication;
import com.example.bankandroid.Models.ApplicationStatus;
import com.example.bankandroid.Models.CreditAgreement;
import com.example.bankandroid.Models.CreditApplication;
import com.example.bankandroid.R;
import com.example.bankandroid.RecyclerAdapters.AccountApplicationRecyclerAdapter;
import com.example.bankandroid.RecyclerAdapters.CreditAgreementRecyclerAdapter;
import com.example.bankandroid.RecyclerAdapters.CreditApplicationRecyclerAdapter;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class CreditHistoryFragment extends Fragment {
    ApiInterface Interface;
    ArrayList<String> array = new ArrayList<String>();;
    Spinner sortStatus;
    int idUs;
    RecyclerView rvA, rv;
    CreditApplicationRecyclerAdapter adapterA;
    CreditAgreementRecyclerAdapter adapter;
    ImageView down;
    EditText sv;


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_credit_history, container, false);
        sortStatus = root.findViewById(R.id.sortStatus);
        rvA = root.findViewById(R.id.credit_application_list);
        rv = root.findViewById(R.id.credit_agreement_list);
        down = root.findViewById(R.id.downTran2);
        sv = root.findViewById(R.id.sv);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        idUs = sharedPreferences.getInt("id", 0);

        Call<ArrayList<ApplicationStatus>> getApplicationStatus = Interface.getAppStat();
        getApplicationStatus.enqueue(new Callback<ArrayList<ApplicationStatus>>() {
            @Override
            public void onResponse(Call<ArrayList<ApplicationStatus>> call, Response<ArrayList<ApplicationStatus>> response) {
                if (response.isSuccessful()){
                    for (int i = response.body().size() - 1; i >= 0; i--){
                        array.add(response.body().get(i).getNameStatus());
                    }

                    ArrayAdapter<String> adapter = new ArrayAdapter<String>(getContext(), android.R.layout.simple_spinner_item, array);
                    adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                    sortStatus.setAdapter(adapter);

                }else {
                    Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<ArrayList<ApplicationStatus>> call, Throwable t) {
                Log.println(Log.ERROR, "Parsing", t.getMessage());
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }

        });

        sortStatus.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                Call<ArrayList<CreditApplication>> getAppl = Interface.getCreditApplication(idUs, position+1, null);
                getAppl.enqueue(new Callback<ArrayList<CreditApplication>>() {
                    @Override
                    public void onResponse(Call<ArrayList<CreditApplication>> call, Response<ArrayList<CreditApplication>> response) {
                        rvA.setLayoutManager(new LinearLayoutManager(getContext()));
                        rvA.setHasFixedSize(true);
                        ArrayList<CreditApplication> listGame = response.body();
                        if(listGame != null) {
                            adapterA = new CreditApplicationRecyclerAdapter(getContext(), listGame);
                            rvA.setAdapter(adapterA);
                        } else {
                            Toast.makeText(getContext(), "Не найдено данных.", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<CreditApplication>> call, Throwable t) {
                        Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                    }
                });
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        sv.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                getCredAg(s.toString().isEmpty() ? null : s.toString());
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

        return root;
    }

    private void getCredAg(String searchString) {
        Call<ArrayList<CreditAgreement>> getCredAg = Interface.getCreditAgreements(idUs, searchString, true);
        getCredAg.enqueue(new Callback<ArrayList<CreditAgreement>>() {
            @Override
            public void onResponse(Call<ArrayList<CreditAgreement>> call, Response<ArrayList<CreditAgreement>> response) {
                handleResponse(response);
            }

            @Override
            public void onFailure(Call<ArrayList<CreditAgreement>> call, Throwable t) {
                handleError(t);
            }
        });
    }

    private void handleResponse(Response<ArrayList<CreditAgreement>> response) {
        if (response.isSuccessful()){
            prepareRecycler(response.body());
            if(response.body() == null){
                    Toast.makeText(getContext(), "Не найдено данных.", Toast.LENGTH_SHORT).show();
            }
        } else {
            Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
        }
    }

    private void handleError(Throwable t) {
        Log.println(Log.ERROR, "Parsing", t.getMessage());
        Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
    }

    private void prepareRecycler(ArrayList<CreditAgreement> listGame) {
        rv.setLayoutManager(new LinearLayoutManager(getContext()));
        rv.setHasFixedSize(true);
        adapter = new CreditAgreementRecyclerAdapter(getContext(), listGame);
        rv.setAdapter(adapter);
    }
}