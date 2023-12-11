package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.content.SharedPreferences;
import android.os.Bundle;

import android.util.Log;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.bankandroid.Models.Account;
import com.example.bankandroid.R;
import com.example.bankandroid.RecyclerAdapters.AccountBlockRecyclerAdapter;
import com.example.bankandroid.RecyclerAdapters.AccountUNBlockRecyclerAdapter;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class BlockAccountFragment extends Fragment {
    Spinner cradblock_reasonSpinner;
    ImageView down;
    RecyclerView rv, UNrv;
    Button blockcard, UNblock;
    ApiInterface Interface;
    AccountBlockRecyclerAdapter adapterC;
    AccountUNBlockRecyclerAdapter adapter2;
    int idUs;
    String[] idsToArchive = new String[] {};
    String[] idsToArchive2 = new String[] {};

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root =inflater.inflate(R.layout.fragment_block_account, container, false);

        cradblock_reasonSpinner = root.findViewById(R.id.accblock_reasonSpinner);
        blockcard = root.findViewById(R.id.buttonBlockAcc);
        rv = root.findViewById(R.id.rvAccBlocks);
        down = root.findViewById(R.id.downSet);
        UNrv = root.findViewById(R.id.rvAccUNBlocks);
        UNblock = root.findViewById(R.id.buttonUNBlockAcc);

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

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getContext(), R.array.block_array, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        cradblock_reasonSpinner.setAdapter(adapter);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        String token = sharedPreferences.getString("token", "");
        idUs = sharedPreferences.getInt("id", 0);

        rv.setLayoutManager(new LinearLayoutManager(getContext()));
        rv.setHasFixedSize(true);

        UNrv.setLayoutManager(new LinearLayoutManager(getContext()));
        UNrv.setHasFixedSize(true);

        fetchDataAndUpdateView();

        blockcard.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ArrayList<String> idsList = new ArrayList<>();
                for (int i=0; i<adapterC.getItemCount(); i++) {
                    if (adapterC.checked.containsKey(i) && adapterC.checked.get(i)) {
                        idsList.add(adapterC.list.get(i).getIdAccount());
                    }
                }
                idsToArchive = idsList.toArray(new String[0]);

                Call<ArrayList<Account>> call = Interface.deleteAccounts(idsToArchive, cradblock_reasonSpinner.getSelectedItem().toString());

                call.enqueue(new Callback<ArrayList<Account>>() {
                    @Override
                    public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                        if(response.isSuccessful()){
                            fetchDataAndUpdateView();
                        } else {
                            Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                        Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                    }
                });
            }
        });

        UNblock.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ArrayList<String> idsList2 = new ArrayList<>();
                for (int i=0; i<adapter2.getItemCount(); i++) {
                    if (adapter2.checked.containsKey(i) && adapter2.checked.get(i)) {
                        idsList2.add(adapter2.list.get(i).getIdAccount());
                    }
                }
                idsToArchive2 = idsList2.toArray(new String[0]);

                Call<ArrayList<Account>> call = Interface.returnAccounts(idsToArchive2);

                call.enqueue(new Callback<ArrayList<Account>>() {
                    @Override
                    public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                        if(response.isSuccessful()){
                            fetchDataAndUpdateView();
                        } else {
                            Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                        Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                    }
                });
            }
        });
        return root;
    }
    private void fetchDataAndUpdateView() {
        Call<ArrayList<Account>> getAccounts = Interface.getAccount(idUs, true);

        getAccounts.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()) {
                    ArrayList<Account> listGame = response.body();
                    adapterC = new AccountBlockRecyclerAdapter(getContext(), listGame);
                    rv.setAdapter(adapterC);
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                Log.println(Log.ERROR, "DataError", t.getMessage());
            }
        });

        Call<ArrayList<Account>> getAccounts2 = Interface.getAccount(idUs, false);

        getAccounts2.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()) {
                    ArrayList<Account> listGame2 = response.body();
                    adapter2 = new AccountUNBlockRecyclerAdapter(getContext(), listGame2);
                    UNrv.setAdapter(adapter2);
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

    }
}