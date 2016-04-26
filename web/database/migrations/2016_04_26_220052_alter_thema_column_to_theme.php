<?php

use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AlterThemaColumnToTheme extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('projects', function (Blueprint $table) {
            $table->dropForeign('projects_thema_id_foreign');
            $table->dropColumn('thema_id');

            $table->integer('theme_id')->unsigned()->nullable(); 
            $table->foreign('theme_id')->references('id')->on('themes')->onDelete('SET NULL');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('projects', function (Blueprint $table) {
            $table->dropForeign('projects_theme_id_foreign');
            $table->dropColumn('theme_id');

            $table->integer('thema_id')->unsigned()->nullable(); 
            $table->foreign('thema_id')->references('id')->on('themes')->onDelete('SET NULL');
        });
    }
}
