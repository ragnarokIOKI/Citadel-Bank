package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.content.SharedPreferences;
import android.os.Bundle;

import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.bankandroid.Models.AccountApplication;
import com.example.bankandroid.Models.ApplicationStatus;
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

public class AccountApplicationCheckFragment extends Fragment {
    ApiInterface Interface;
    ArrayList<String> array = new ArrayList<String>();;
    Spinner sortStatus;
    RecyclerView rv;
    AccountApplicationRecyclerAdapter adapter;
    int idUs;
    ImageView down;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root = inflater.inflate(R.layout.fragment_account_application_check, container, false);
        sortStatus = root.findViewById(R.id.sortStatusAcc);
        rv = root.findViewById(R.id.acc_application_list);
        down = root.findViewById(R.id.downTran2);

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
                //String chosenSorting = parent.getItemAtPosition(position).toString();

                Call<ArrayList<AccountApplication>> getAccApp = Interface.getAccountApplication(idUs, position+1);
                getAccApp.enqueue(new Callback<ArrayList<AccountApplication>>() {
                    @Override
                    public void onResponse(Call<ArrayList<AccountApplication>> call, Response<ArrayList<AccountApplication>> response) {
                        rv.setLayoutManager(new LinearLayoutManager(getContext()));
                        rv.setHasFixedSize(true);
                        ArrayList<AccountApplication> listGame = response.body();
                        if(listGame != null) {
                            adapter = new AccountApplicationRecyclerAdapter(getContext(), listGame);
                            rv.setAdapter(adapter);
                        } else {
                            Toast.makeText(getContext(), "Не найдено данных.", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<AccountApplication>> call, Throwable t) {
                        Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                    }
                });

            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
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
}