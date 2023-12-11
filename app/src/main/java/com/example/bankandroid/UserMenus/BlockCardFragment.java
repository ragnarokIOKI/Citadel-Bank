package com.example.bankandroid.UserMenus;

import static android.content.Context.MODE_PRIVATE;

import android.content.SharedPreferences;
import android.os.Bundle;

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
import com.example.bankandroid.Models.Card;
import com.example.bankandroid.R;
import com.example.bankandroid.RecyclerAdapters.CardBlockRecyclerAdapter;
import com.example.bankandroid.RecyclerAdapters.CardUNBlockRecyclerAdapter;
import com.example.bankandroid.Utility.ApiInterface;
import com.example.bankandroid.Utility.Configurator;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class BlockCardFragment extends Fragment {
    Spinner cradblock_reasonSpinner;
    RecyclerView rv, UNrv;
    Button blockcard, UNblock;
    ApiInterface Interface;
    ImageView down;
    CardBlockRecyclerAdapter adapterC;
    CardUNBlockRecyclerAdapter adapter2;
    int idUs;
    String[] idsToArchive = new String[] {};
    String[] idsToArchive2 = new String[] {};

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View root =inflater.inflate(R.layout.fragment_block_card, container, false);

        cradblock_reasonSpinner = root.findViewById(R.id.cradblock_reasonSpinner);
        blockcard = root.findViewById(R.id.buttonBlockCard);
        rv = root.findViewById(R.id.rvCardBlocks);
        down = root.findViewById(R.id.downSet);
        UNrv = root.findViewById(R.id.rvCardUNBlocks);
        UNblock = root.findViewById(R.id.buttonUNBlockCard);

        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(getContext(), R.array.block_array, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        cradblock_reasonSpinner.setAdapter(adapter);

        Interface = Configurator.buildRequest().create(ApiInterface.class);

        SharedPreferences sharedPreferences = getActivity().getSharedPreferences("myPrefs", MODE_PRIVATE);
        idUs = sharedPreferences.getInt("id", 0);

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

        Call<ArrayList<Account>> getAccounts = Interface.getAccount(idUs, true);
        getAccounts.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()) {

                    ArrayList<Card> allCards = new ArrayList<>();
                    rv.setLayoutManager(new LinearLayoutManager(getContext()));
                    rv.setHasFixedSize(true);
                    adapterC = new CardBlockRecyclerAdapter(getContext(), allCards);
                    rv.setAdapter(adapterC);

                    for (Account account : response.body()) {
                        String accountNumber = account.getIdAccount();
                        Call<ArrayList<Card>> getCards = Interface.getCard(accountNumber, true);
                        getCards.enqueue(new Callback<ArrayList<Card>>() {

                            @Override
                            public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                                if(response.isSuccessful()){
                                    ArrayList<Card> listCards = response.body();
                                    for (Card card : listCards) {
                                        if (!containsCard(allCards, card)) {
                                            allCards.add(card);
                                        }
                                    }
                                    adapterC.notifyDataSetChanged();
                                }
                            }

                            @Override
                            public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
                                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                            }
                        });
                    }

                }
            }
            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        Call<ArrayList<Account>> getBlockAccounts = Interface.getAccount(idUs, false);
        getBlockAccounts.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()) {

                    ArrayList<Card> allUNCards = new ArrayList<>();
                    UNrv.setLayoutManager(new LinearLayoutManager(getContext()));
                    UNrv.setHasFixedSize(false);
                    adapter2 = new CardUNBlockRecyclerAdapter(getContext(), allUNCards);
                    UNrv.setAdapter(adapter2);

                    for (Account account : response.body()) {
                        String accountNumber = account.getIdAccount();
                        Call<ArrayList<Card>> getCards = Interface.getCard(accountNumber, false);
                        getCards.enqueue(new Callback<ArrayList<Card>>() {

                            @Override
                            public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                                if(response.isSuccessful()){
                                    ArrayList<Card> listCards = response.body();
                                    allUNCards.addAll(listCards);
                                    adapter2.notifyDataSetChanged();
                                }
                            }

                            @Override
                            public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
                                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                            }
                        });
                    }

                }
            }
            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        blockcard.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ArrayList<String> idsList = new ArrayList<>();
                for (int i=0; i<adapterC.getItemCount(); i++) {
                    if (adapterC.checked.containsKey(i) && adapterC.checked.get(i)) {
                        idsList.add(adapterC.list.get(i).getIdCard());
                    }
                }
                idsToArchive = idsList.toArray(new String[0]);

                Call<ArrayList<Card>> call = Interface.deleteCards(idsToArchive, cradblock_reasonSpinner.getSelectedItem().toString());

                call.enqueue(new Callback<ArrayList<Card>>() {
                    @Override
                    public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                        if(response.isSuccessful()){
                            fetchDataAndUpdateView();
                        } else {
                            Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
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
                        idsList2.add(adapter2.list.get(i).getIdCard());
                    }
                }
                idsToArchive2 = idsList2.toArray(new String[0]);

                Call<ArrayList<Card>> call = Interface.returnCards(idsToArchive2);

                call.enqueue(new Callback<ArrayList<Card>>() {
                    @Override
                    public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                        if(response.isSuccessful()){
                            fetchDataAndUpdateView();
                        } else {
                            Toast.makeText(getContext(), "Ошибка клиента", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
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
                    ArrayList<Card> allCards = new ArrayList<>();
                    for (Account account : response.body()) {
                        String accountNumber = account.getIdAccount();
                        Call<ArrayList<Card>> getCards = Interface.getCard(accountNumber, true);
                        getCards.enqueue(new Callback<ArrayList<Card>>() {

                            @Override
                            public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                                if(response.isSuccessful()){
                                    ArrayList<Card> listCards = response.body();
                                    for (Card card : listCards) {
                                        if (!containsCard(allCards, card)) {
                                            allCards.add(card);
                                        }
                                    }
                                    adapterC = new CardBlockRecyclerAdapter(getContext(), allCards);
                                    rv.setAdapter(adapterC);
                                }
                            }

                            @Override
                            public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
                                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                            }
                        });
                    }

                }
            }
            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });

        Call<ArrayList<Account>> getBlockAccounts = Interface.getAccount(idUs, false);
        getBlockAccounts.enqueue(new Callback<ArrayList<Account>>() {
            @Override
            public void onResponse(Call<ArrayList<Account>> call, Response<ArrayList<Account>> response) {
                if (response.isSuccessful()) {
                    ArrayList<Card> allUNCards = new ArrayList<>();
                    for (Account account : response.body()) {
                        String accountNumber = account.getIdAccount();
                        Call<ArrayList<Card>> getCards = Interface.getCard(accountNumber, false);
                        getCards.enqueue(new Callback<ArrayList<Card>>() {

                            @Override
                            public void onResponse(Call<ArrayList<Card>> call, Response<ArrayList<Card>> response) {
                                if(response.isSuccessful()){
                                    allUNCards.clear();
                                    ArrayList<Card> listCards = response.body();
                                    allUNCards.addAll(listCards);
                                    adapter2 = new CardUNBlockRecyclerAdapter(getContext(), allUNCards);
                                    UNrv.setAdapter(adapter2);
                                }
                            }

                            @Override
                            public void onFailure(Call<ArrayList<Card>> call, Throwable t) {
                                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
                            }
                        });
                    }

                }
            }
            @Override
            public void onFailure(Call<ArrayList<Account>> call, Throwable t) {
                Toast.makeText(getContext(), t.getMessage(), Toast.LENGTH_SHORT).show();
            }
        });
    }
    private boolean containsCard(ArrayList<Card> list, Card card){
        for (Card c : list){
            if (c.getIdCard().equals(card.getIdCard())){
                return true;
            }
        }
        return false;
    }
}