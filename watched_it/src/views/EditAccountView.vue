<script setup lang="ts">
import { MDCTextField } from '@material/textfield';
import { MDCRipple } from '@material/ripple';
import { onMounted } from 'vue';

import SimpleHeader from '@/components/SimpleHeader.vue';

onMounted(() => {
    const inputs = document.getElementsByClassName('mdc-text-field');
    const btn = document.querySelector('.mdc-button');
    if (inputs) {
        for (let input of inputs) {
            let textField = new MDCTextField(input);
        }
    }
    if (btn) {
        const buttonRipple = new MDCRipple(btn);
    }
});
</script>

<template>
    <SimpleHeader :currentTitle="'Edit Account'" />
    <section class="login">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="logo-wrapper">
                        <img src="@/assets/logo.png" alt="logo">
                    </div>
                    <div class="title-wrap">
                        <h1>Edit Account</h1>
                    </div>
                </div>
                <form @submit.prevent="submit">
                    <div class="col-md-12">
                        <div class="input-wrapper">
                            <label :class="classNameError" class="mdc-text-field mdc-text-field--outlined">
                                <span class="mdc-notched-outline">
                                    <span class="mdc-notched-outline__leading"></span>
                                    <span class="mdc-notched-outline__notch">
                                        <span class="mdc-floating-label" id="my-label-id">Full name</span>
                                    </span>
                                    <span class="mdc-notched-outline__trailing"></span>
                                </span>
                                <input type="text" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                    v-model="name">
                            </label>
                            <div class="mdc-text-field-helper-line">
                                <div class="mdc-text-field-helper-text mdc-text-field-helper-text--validation-msg">
                                    {{ nameError }}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="input-wrapper">
                            <label :class="classEmailError" class="mdc-text-field mdc-text-field--outlined">
                                <span class="mdc-notched-outline">
                                    <span class="mdc-notched-outline__leading"></span>
                                    <span class="mdc-notched-outline__notch">
                                        <span class="mdc-floating-label" id="my-label-id">Email</span>
                                    </span>
                                    <span class="mdc-notched-outline__trailing"></span>
                                </span>
                                <input type="email" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                    v-model="email">
                            </label>
                            <div class="mdc-text-field-helper-line">
                                <div class="mdc-text-field-helper-text mdc-text-field-helper-text--validation-msg">
                                    {{ emailError }}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="input-wrapper">
                            <label :class="classphoneError" class="mdc-text-field mdc-text-field--outlined">
                                <span class="mdc-notched-outline">
                                    <span class="mdc-notched-outline__leading"></span>
                                    <span class="mdc-notched-outline__notch">
                                        <span class="mdc-floating-label" id="my-label-id">Phone number</span>
                                    </span>
                                    <span class="mdc-notched-outline__trailing"></span>
                                </span>
                                <input type="tel" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                    v-model="phone">
                            </label>
                            <div class="mdc-text-field-helper-line">
                                <div class="mdc-text-field-helper-text mdc-text-field-helper-text--validation-msg">
                                    {{ phoneError }}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="input-wrapper">
                            <label :class="classPassError" class="mdc-text-field mdc-text-field--outlined">
                                <span class="mdc-notched-outline">
                                    <span class="mdc-notched-outline__leading"></span>
                                    <span class="mdc-notched-outline__notch">
                                        <span class="mdc-floating-label" id="my-label-id">Password</span>
                                    </span>
                                    <span class="mdc-notched-outline__trailing"></span>
                                </span>
                                <input type="password" class="mdc-text-field__input" aria-labelledby="my-label-id" required
                                    minlength="10" v-model="password">
                            </label>
                            <div class="mdc-text-field-helper-line">
                                <div class="mdc-text-field-helper-text mdc-text-field-helper-text--validation-msg">
                                    {{ passwordError }}
                                </div>
                            </div>
                            <p>Leave empty if you want to keep your current password</p>
                        </div>
                    </div>
                    <div class="col-md-12">
                            <div class="mdc-touch-target-wrapper btn-wrap">
                            <button class="mdc-button mdc-button--raised">
                                <span class="mdc-button__ripple"></span>
                                <span class="mdc-button__touch"></span>
                                <span class="mdc-button__label">Save</span>
                            </button>
                        </div>
                        <div class="mdc-touch-target-wrapper btn-wrap white">
                            <button class="mdc-button mdc-button--raised">
                                <span class="mdc-button__ripple"></span>
                                <span class="mdc-button__touch"></span>
                                <span class="mdc-button__label">Cancel</span>
                            </button>
                        </div> 
                    </div>
                </form>
            </div>
        </div>
    </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import {
    validateName, validateEmail, validatePhone, errNameEmp, errName,
    errEmailEmp, errEmail, errPhoneEmp, errPhone, errPassEmp, errPass, errConfirmPass
} from '@/assets/javascript/validation';
import { signUp, signIn } from '@/assets/javascript/api';
import { User } from '@/assets/javascript/Models/UserInterface';

export default defineComponent({
    data() {
        return {
            name: '',
            email: '',
            phone: '',
            password: '',
            confirmPassword: '',
            nameError: 'Invalid name.',
            emailError: 'Invalid email.',
            phoneError: 'Invalid phone number.',
            passwordError: 'Invalid password',
            confirmError: 'Password must match.',
            classNameError: '',
            classEmailError: '',
            classphoneError: '',
            classPassError: '',
            classConfirmError: '',
        }
    },
    methods: {
        async submit() {
            this.checkName()
            this.checkEmail()
            this.checkPhone()
            this.checkPassword()
            this.checkConfirm()

            if (this.nameError == '', this.emailError == '',
                this.phoneError == '', this.passwordError == '',
                this.confirmError == '') {
                const user: User = {
                    id: null,
                    name: this.name,
                    email: this.email,
                    phone: this.phone,
                    password: this.password
                }

                const result = await signUp(user);

                if (result?.code == 201 && result.data.id) {
                    let newUser: User = result.data
                    newUser.password = this.password

                    const res = await signIn(newUser)

                    if (res?.code == 200 && res.data.id) {
                        localStorage.setItem('user', res.data.id?.toString())
                        this.$router.push('account');
                    } else {
                        console.log(res)
                    }
                }
                else {
                    console.log(result)
                }
            }
        },
        checkName() {
            this.nameError = this.name.length == 0 ? errNameEmp() :
                (validateName(this.name) ? '' : errName(this.name))
            if (this.nameError) {
                this.classNameError = 'field-error'
            }
            else {
                this.classNameError = ''
            }
        },
        checkEmail() {
            this.emailError = this.email.length == 0 ? errEmailEmp() :
                (validateEmail(this.email) ? '' : errEmail(this.email))
            if (this.emailError) {
                this.classEmailError = 'field-error'
            }
            else {
                this.classEmailError = ''
            }
        },
        checkPhone() {
            this.phoneError = this.phone.length == 0 ? errPhoneEmp() : (
                validatePhone(this.phone) ? '' : errPhone(this.phone)
            )
            if (this.phoneError) {
                this.classphoneError = 'field-error'
            }
            else {
                this.classphoneError = ''
            }
        },
        checkPassword() {
            this.passwordError = this.password.length == 0 ? errPassEmp() : (
                this.password.length == 10 ? '' : errPass()
            )
            if (this.passwordError) {
                this.classPassError = 'field-error'
            }
            else {
                this.classPassError = ''
            }
        },
        checkConfirm() {
            this.confirmError = this.confirmPassword.length == 0 ? errConfirmPass() : (
                this.confirmPassword == this.password ? '' : errConfirmPass()
            )
            if (this.confirmError) {
                this.classConfirmError = 'field-error'
            }
            else {
                this.classConfirmError = ''
            }
        }
    },
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/colors.scss";
@import "@/assets/styles/components/buttons.scss";
@import "@/assets/styles/components/input.scss";
@import "@/assets/styles/pages/editaccount.scss";
</style>