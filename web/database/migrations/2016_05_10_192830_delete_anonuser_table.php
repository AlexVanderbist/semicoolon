<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class DeleteAnonuserTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('opinions', function (Blueprint $table) {
            $table->dropForeign('opinions_anon_user_id_foreign');
            $table->dropColumn('anon_user_id');
        });
        Schema::drop('anonymous_user');
        
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::create('anonymous_user', function (Blueprint $table) {
            $table->increments('id');
            $table->integer('sex');
            $table->integer('birthyear');
            $table->string('city');
        });
    }
}
