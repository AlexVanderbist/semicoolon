<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class PropositionOpinions extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('proposition_opinions', function (Blueprint $table) {
            $table->increments('id');
            $table->integer('type');
            $table->integer('value');
            $table->integer('user_id')->unsigned();
            $table->integer('proposition_id')->unsigned();
            $table->timestamps();


            $table->foreign('user_id')->references('id')->on('users')->onDelete('cascade');
            $table->foreign('proposition_id')->references('id')->on('propositions')->onDelete('cascade');
            
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::drop('proposition_opinions');
    }
}
